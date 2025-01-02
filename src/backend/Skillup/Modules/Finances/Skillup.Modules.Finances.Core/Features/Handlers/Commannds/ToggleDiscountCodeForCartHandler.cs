using MediatR;
using Microsoft.IdentityModel.Tokens;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class ToggleDiscountCodeForCartHandler(ICartRepository cartRepository, IDiscountCodeRepository discountCodeRepository) : IRequestHandler<ToggleDiscountCodeForCartRequest>
    {
        private readonly ICartRepository _cartRepository = cartRepository;
        private readonly IDiscountCodeRepository _discountCodeRepository = discountCodeRepository;

        public async Task Handle(ToggleDiscountCodeForCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCart(request.CartId) ?? throw new NotFoundException($"Cart with ID {request.CartId} not found");

            if (request.DiscountCode.IsNullOrEmpty())
            {
                cart.Total = cart.Items.Sum(x => x.Item.Price);
                cart.Items.ToList().ForEach(item => item.Price = item.Item.Price);
                cart.DiscountCode = null;
                cart.DiscountCodeId = null;
            }
            else
            {
                var discountCode = await _discountCodeRepository.GetByCode(request.DiscountCode!) ?? throw new NotFoundException($"DiscountCode with Code {request.DiscountCode} not found");
                cart.ApplyDiscountCode(discountCode);
            }

            await _cartRepository.Update(cart);
        }
    }
}
