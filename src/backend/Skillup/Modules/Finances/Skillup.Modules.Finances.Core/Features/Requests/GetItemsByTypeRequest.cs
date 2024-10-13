using MediatR;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Features.Requests
{
    record class GetItemsByTypeRequest(ItemType Type) : IRequest<IEnumerable<Item>>;
}
