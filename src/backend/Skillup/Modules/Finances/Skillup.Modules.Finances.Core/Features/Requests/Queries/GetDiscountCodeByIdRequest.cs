using MediatR;
using Skillup.Modules.Finances.Core.DTO;

namespace Skillup.Modules.Finances.Core.Features.Requests.Queries
{
    internal record GetDiscountCodeByIdRequest(Guid DiscountCodeId) : IRequest<DiscountCodeDto>;
}
