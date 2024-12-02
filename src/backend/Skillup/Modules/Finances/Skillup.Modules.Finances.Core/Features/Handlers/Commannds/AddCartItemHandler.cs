using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class AddCartItemHandler(ICartRepository cartRepository) : IRequestHandler<AddCartItemRequest, Guid>
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public async Task<Guid> Handle(AddCartItemRequest request, CancellationToken cancellationToken)
        {
            await _cartRepository.AddCartItem(new CartItem() { CartId = request.CartId, ItemId = request.ItemId });
            return request.CartId;
        }
    }
}
