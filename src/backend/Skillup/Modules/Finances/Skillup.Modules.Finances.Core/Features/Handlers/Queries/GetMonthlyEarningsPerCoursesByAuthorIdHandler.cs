using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetMonthlyEarningsPerCoursesByAuthorIdHandler(IOrderRepository orderRepository) : IRequestHandler<GetMonthlyEarningsPerCoursesByAuthorIdRequest, YearEarningsDto>
    {
        private readonly IOrderRepository orderRepository = orderRepository;

        public async Task<YearEarningsDto> Handle(GetMonthlyEarningsPerCoursesByAuthorIdRequest request, CancellationToken cancellationToken)
        {
            var orderItems = await orderRepository.GetOrderItemsForAuthor(request.AuthorId);

            return new YearEarningsDto(request.Year, orderItems);
        }
    }
}
