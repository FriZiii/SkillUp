using MediatR;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Features.Requests.Queries
{
    internal record GetPurchaseHistoriesByUserIdRequest(Guid UserId) : IRequest<IEnumerable<IGrouping<Item, PurchaseHistory>>>;
}
