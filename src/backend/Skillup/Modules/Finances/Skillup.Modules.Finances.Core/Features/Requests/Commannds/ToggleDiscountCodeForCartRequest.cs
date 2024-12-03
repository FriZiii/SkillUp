using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record ToggleDiscountCodeForCartRequest(Guid CartId, string? DiscountCode) : IRequest;
}
