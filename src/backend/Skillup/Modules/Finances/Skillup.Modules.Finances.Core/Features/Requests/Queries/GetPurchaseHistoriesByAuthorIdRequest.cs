using MediatR;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Features.Requests.Queries
{
    internal record GetPurchaseHistoriesByAuthorIdRequest(Guid AuthorId) : IRequest<IEnumerable<IGrouping<Item, PurchaseHistory>>>;
}
