using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetRevenueByAuthorIdHandler(IOrderRepository orderRepository, IClock clock) : IRequestHandler<GetRevenueByAuthorIdRequest, RevenueDto>
    {
        private readonly IOrderRepository orderRepository = orderRepository;
        private readonly IClock clock = clock;

        public async Task<RevenueDto> Handle(GetRevenueByAuthorIdRequest request, CancellationToken cancellationToken)
        {
            var orderItemsByAuthor = await orderRepository.GetOrderItemsForAuthor(request.AuthorId);

            var currentWeekStart = clock.CurrentDate().AddDays(-7);
            var previousWeekStart = clock.CurrentDate().AddDays(-14);
            var previousWeekEnd = clock.CurrentDate().AddDays(-7);

            var currentWeekRevenue = orderItemsByAuthor
                .Where(x => x.Order.Created >= currentWeekStart && x.Order.Created <= DateTime.UtcNow)
                .Sum(x => x.ItemPrice);

            var previousWeekRevenue = orderItemsByAuthor
                .Where(x => x.Order.Created >= previousWeekStart && x.Order.Created < previousWeekEnd)
                .Sum(x => x.ItemPrice);

            var changePercentage = previousWeekRevenue == 0
                ? (currentWeekRevenue > 0 ? 100 : 0)
                : ((currentWeekRevenue - previousWeekRevenue) / previousWeekRevenue) * 100;

            return new RevenueDto()
            {
                BeginDate = orderItemsByAuthor.OrderBy(x => x.Order.Created).First().Order.Created,
                ItemsCovered = orderItemsByAuthor.GroupBy(x => x.ItemId).Count(),
                TotalRevenue = orderItemsByAuthor.Sum(x => x.ItemPrice),
                ChangePercentage = changePercentage,
                LastWeekRevenue = previousWeekRevenue
            };
        }
    }
}
