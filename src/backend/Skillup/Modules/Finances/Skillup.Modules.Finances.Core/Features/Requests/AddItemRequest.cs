using MediatR;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Features.Requests
{
    internal record AddItemRequest(Guid ItemId, Guid AuthorId, ItemType Type) : IRequest;
}
