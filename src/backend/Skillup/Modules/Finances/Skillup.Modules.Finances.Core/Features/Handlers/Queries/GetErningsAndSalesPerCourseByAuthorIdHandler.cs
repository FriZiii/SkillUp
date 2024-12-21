using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetErningsAndSalesPerCourseByAuthorIdHandler(IOrderRepository orderRepository) : IRequestHandler<GetErningsAndSalesPerCourseByAuthorIdRequest, IEnumerable<ItemEarningsDto>>
    {
        private readonly IOrderRepository orderRepository = orderRepository;

        public async Task<IEnumerable<ItemEarningsDto>> Handle(GetErningsAndSalesPerCourseByAuthorIdRequest request, CancellationToken cancellationToken)
        {
            var ordersItemsPerAuthor = await orderRepository.GetOrderItemsForAuthor(request.AuthorId);
            var itemEarnings = ordersItemsPerAuthor
             .GroupBy(oi => oi.ItemId)
             .Select(group => new ItemEarningsDto
             {
                 ItemId = group.Key,
                 ItemsCount = group.Count(),
                 Total = group.Sum(g => g.ItemPrice.Amount)
             })
             .ToList();

            return itemEarnings;
        }
    }
}
