using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record DeleteItemFromCartRequest(Guid CartId, Guid ItemId) : IRequest;
}
