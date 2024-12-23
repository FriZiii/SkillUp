using MassTransit;
using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Events.Finances;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class CheckoutCartHandler(IWalletRepository walletRepository,
            ICartRepository cartRepository,
            IOrderRepository orderRepository,
            IPublishEndpoint publishEndpoint,
            IClock clock)
        : IRequestHandler<CheckoutCartRequest>
    {
        private readonly IWalletRepository _walletRepository = walletRepository;
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IClock _clock = clock;

        public async Task Handle(CheckoutCartRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWallet(request.WalletId) ?? throw new Exception(); // TODO: Custom Ex: wallet with id doesnt exist
            var cart = await _cartRepository.GetCart(request.CartId) ?? throw new Exception(); // TODO: Custom Ex: cart with id doesnt exist

            var orderId = Guid.NewGuid();
            wallet.SubtractFromBalance(cart.Total);
            var history = new BalanceHistory(wallet.Id, wallet.Balance, $"Order number SU-{orderId.ToString("N")[..8]}", "Substract");

            await _walletRepository.UpdateWalletBalance(wallet, history);

            await _cartRepository.Delete(cart);

            var order = new Order()
            {
                Id = orderId,
                OrdererId = wallet.OwnerId,
                Created = _clock.CurrentDate(),
                TotalPrice = cart.Total,
                BalanceHistoryId = history.Id,
            };

            var orderItems = cart.Items.Select(x => new OrderItem() { ItemId = x.ItemId, ItemPrice = x.Price, OrderId = order.Id }).ToList();
            order.Items = orderItems;

            await _orderRepository.Add(order);

            foreach (var item in order.Items)
            {
                await PublishItemPurchasedEvent(item.Item, wallet.OwnerId, cancellationToken);
            }
        }

        private async Task PublishItemPurchasedEvent(Item item, Guid userId, CancellationToken cancellationToken)
        {
            switch (item.Type)
            {
                case ItemType.Course:
                    await _publishEndpoint.Publish(new CoursePurchased(item.Id, userId, item.AuthorId), cancellationToken);
                    break;
                default:
                    break;
            }
        }
    }
}
