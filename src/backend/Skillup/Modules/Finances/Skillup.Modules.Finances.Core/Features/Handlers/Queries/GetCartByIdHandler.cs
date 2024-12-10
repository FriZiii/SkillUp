using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetCartByIdHandler(ICartRepository cartRepository) : IRequestHandler<GetCartByIdRequest, CartDto>
    {
        private readonly ICartRepository _cartRepository = cartRepository;

        public async Task<CartDto> Handle(GetCartByIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CartMapper();
            var cart = await _cartRepository.GetCart(request.CartId) ?? throw new Exception();
            return mapper.CartToDto(cart!);
        }
    }
}
