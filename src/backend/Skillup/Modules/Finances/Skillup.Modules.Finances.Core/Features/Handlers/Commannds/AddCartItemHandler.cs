using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class AddCartItemHandler(ICartRepository cartRepository, IItemRepository itemRepository) : IRequestHandler<AddCartItemRequest>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IItemRepository _itemRepository = itemRepository;

        public async Task Handle(AddCartItemRequest request, CancellationToken cancellationToken)
        {
            if (request.CartId is not null)
            {
                var cart = await _cartRepository.GetCart((Guid)request.CartId) ?? throw new NotFoundException($"Cart with ID {request.CartId} not found");
            }
            else
            {
                request.CartId = Guid.NewGuid();
            }

            var item = await _itemRepository.GetById(request.ItemId) ?? throw new NotFoundException($"Item with ID {request.ItemId} not found");

            await _cartRepository.AddCartItem(
                new CartItem()
                {
                    CartId = (Guid)request.CartId,
                    ItemId = request.ItemId,
                    Item = item,
                    Price = item.Price
                }
             );

            var updatedCart = await _cartRepository.GetCart((Guid)request.CartId);
            if (updatedCart != null && updatedCart.DiscountCode != null)
            {
                updatedCart.ApplyDiscountCode(updatedCart.DiscountCode);
                await _cartRepository.Update(updatedCart);
            }
        }
    }
}
