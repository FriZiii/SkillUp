using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetPurchaseHistoriesByAuthorIdHandler : IRequestHandler<GetPurchaseHistoriesByAuthorIdRequest, IEnumerable<IGrouping<Item, PurchaseHistory>>>
    {
        private readonly IPurchaseHistoryRepository _purchaseHistoryRepository;

        public GetPurchaseHistoriesByAuthorIdHandler(IPurchaseHistoryRepository purchaseHistoryRepository)
        {
            _purchaseHistoryRepository = purchaseHistoryRepository;
        }

        public async Task<IEnumerable<IGrouping<Item, PurchaseHistory>>> Handle(GetPurchaseHistoriesByAuthorIdRequest request, CancellationToken cancellationToken)
        {
            var histories = await _purchaseHistoryRepository.GetByAuthorId(request.AuthorId);
            return histories;
        }
    }
}
