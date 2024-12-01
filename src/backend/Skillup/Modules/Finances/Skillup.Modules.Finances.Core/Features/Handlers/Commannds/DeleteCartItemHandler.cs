using MediatR;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class DeleteCartItemHandler(ICartRepository cartRepository) : IRequestHandler<DeleteCartItemRequest>
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public async Task Handle(DeleteCartItemRequest request, CancellationToken cancellationToken)
        {
            await _cartRepository.DeleteCartItem(request.CartItemId);
        }
    }
}
