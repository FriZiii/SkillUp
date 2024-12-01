using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record ToggleDiscountCodeForItemRequest(Guid DiscountCodeId, Guid ItemId) : IRequest;
}
