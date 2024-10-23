using MediatR;
using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Features.Requests.Queries
{
    internal record GetItemByIdRequest(Guid ItemId) : IRequest<Item>;
}
