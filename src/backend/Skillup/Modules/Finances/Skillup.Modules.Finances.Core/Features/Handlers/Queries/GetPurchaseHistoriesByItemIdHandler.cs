using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetPurchaseHistoriesByItemIdHandler : IRequestHandler<GetPurchaseHistoriesByItemIdRequest, IEnumerable<PurchaseHistory>>
    {
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;

        public GetPurchaseHistoriesByItemIdHandler(IPurchaseHistoryRepository purchaseHistoryRepository)
        {
            _purchaseHistoryRepository = purchaseHistoryRepository;
        }

        public async Task<IEnumerable<PurchaseHistory>> Handle(GetPurchaseHistoriesByItemIdRequest request, CancellationToken cancellationToken)
        {
            var histories = await _purchaseHistoryRepository.GetByItemId(request.ItemId);
            return histories;
        }
    }
}
