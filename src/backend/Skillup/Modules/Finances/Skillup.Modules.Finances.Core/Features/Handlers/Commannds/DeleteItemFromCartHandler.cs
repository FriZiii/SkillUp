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
        }
    }
}
