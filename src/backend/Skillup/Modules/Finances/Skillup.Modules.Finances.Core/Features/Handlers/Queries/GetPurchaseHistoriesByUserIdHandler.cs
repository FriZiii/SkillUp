using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetPurchaseHistoriesByUserIdHandler : IRequestHandler<GetPurchaseHistoriesByUserIdRequest, IEnumerable<IGrouping<Item, PurchaseHistory>>>
    {
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;

        public GetPurchaseHistoriesByUserIdHandler(IPurchaseHistoryRepository purchaseHistoryRepository)
        {
            _purchaseHistoryRepository = purchaseHistoryRepository;
        }

        public async Task<IEnumerable<IGrouping<Item, PurchaseHistory>>> Handle(GetPurchaseHistoriesByUserIdRequest request, CancellationToken cancellationToken)
        {
            var histories = await _purchaseHistoryRepository.GetByUserId(request.UserId);
            return histories;
        }
    }
}
