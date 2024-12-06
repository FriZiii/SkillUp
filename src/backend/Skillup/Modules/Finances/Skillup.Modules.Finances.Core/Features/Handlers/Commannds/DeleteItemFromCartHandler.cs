using MediatR;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class DeleteItemFromCartHandler(ICartRepository cartRepository) : IRequestHandler<DeleteItemFromCartRequest>
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public async Task Handle(DeleteItemFromCartRequest request, CancellationToken cancellationToken)
        {
            await _cartRepository.DeleteItemFromCart(request.CartId, request.ItemId);

            var updatedCart = await _cartRepository.GetCart(request.CartId);
            if (updatedCart != null && updatedCart.DiscountCode != null)
            {
                if (!updatedCart.DiscountCode.AppliesToEntireCart)
                {
                    if (!updatedCart.DiscountCode.DiscountedItems.Any(discountedItem => updatedCart.Items.Any(cartItem => cartItem.ItemId == discountedItem.ItemId)))
                    {
                        updatedCart.DiscountCode = null;
                        updatedCart.DiscountCodeId = null;
                        updatedCart.Total = updatedCart.Items.Sum(x => x.Price);

                        await _cartRepository.Update(updatedCart);
                        return;
                    }
                }
                else
                {
                    updatedCart.ApplyDiscountCode(updatedCart.DiscountCode);
                    await _cartRepository.Update(updatedCart);
                }
            }
        }
    }
}
