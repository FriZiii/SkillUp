using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetOrdersByOrdererIdHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrdersByOrdererIdRequest, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<IEnumerable<OrderDto>> Handle(GetOrdersByOrdererIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new OrderMapper();
            var orders = await _orderRepository.GetByOrderer(request.OrdererId);

            return orders.Select(mapper.OrderToDto);
        }
    }
}
