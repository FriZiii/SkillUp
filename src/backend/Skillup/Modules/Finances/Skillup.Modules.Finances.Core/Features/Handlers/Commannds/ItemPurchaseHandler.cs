using MassTransit;
using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Exceptions;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Events.Finances;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class ItemPurchaseHandler : IRequestHandler<ItemPurchaseRequest>
    {
        private readonly IItemRepository _itemRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;
        private readonly IClock _clock;
        private readonly IPublishEndpoint _publishEndpoint;

        public ItemPurchaseHandler(IItemRepository itemRepository, IWalletRepository walletRepository, IPurchaseHistoryRepository purchaseHistoryRepository, IClock clock, IPublishEndpoint publishEndpoint)
        {
            _itemRepository = itemRepository;
            _walletRepository = walletRepository;
            _purchaseHistoryRepository = purchaseHistoryRepository;
            _clock = clock;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(ItemPurchaseRequest request, CancellationToken cancellationToken)
        {
            var item = await _itemRepository.GetById(request.ItemId) ?? throw new ItemNotFoundException(request.ItemId);
            var userWallet = await _walletRepository.GetWalletByUserId(request.UserId) ?? throw new UserNotFoundException(request.UserId);

            if (userWallet.Balance < item.Price) throw new Exception("User dont have enough currency"); //TODO : Custom exception

            await _walletRepository.SubtractBalanceFromWalletByUserId(userWallet.Id, item.Price);
            var purchaseHistory = new PurchaseHistory(item.Id, userWallet.Id, _clock.CurrentDate(), item.Price);
            await _purchaseHistoryRepository.Add(purchaseHistory);

            await PublishItemPurchasedEvent(item, request.UserId, cancellationToken);
        }

        private async Task PublishItemPurchasedEvent(Item item, Guid userId, CancellationToken cancellationToken)
        {
            switch (item.Type)
            {
                case ItemType.Course:
                    await _publishEndpoint.Publish(new CoursePurchased(item.Id, userId), cancellationToken);
                    break;
                default:
                    break;
            }
        }
    }
}
