using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Features.Requests.Queries
{
    internal record GetItemsByTypeRequest(ItemType Type) : IRequest<IEnumerable<ItemDto>>;
}
