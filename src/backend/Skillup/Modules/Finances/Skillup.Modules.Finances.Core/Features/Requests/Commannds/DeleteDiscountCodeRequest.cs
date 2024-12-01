using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record DeleteDiscountCodeRequest(Guid DiscountCodeId) : IRequest;
}
