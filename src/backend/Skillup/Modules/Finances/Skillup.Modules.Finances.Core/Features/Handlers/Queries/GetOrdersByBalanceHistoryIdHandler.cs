using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetOrdersByBalanceHistoryIdHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrdersByBalanceHistoryIdRequest, OrderDto>
    {
        private readonly IOrderRepository _orderRepository = orderRepository;

        public async Task<OrderDto> Handle(GetOrdersByBalanceHistoryIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new OrderMapper();
            var order = await _orderRepository.GetByBalanceHistoryId(request.BalanceHistoryId) ?? throw new Exception(); // TODO: Custom ex: Order with balanceHistoryId doesnt exist
            return mapper.OrderToDto(order);
        }
    }
}
