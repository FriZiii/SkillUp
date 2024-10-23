using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record ItemPurchaseRequest(Guid ItemId, Guid UserId) : IRequest;
}
