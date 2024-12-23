using MediatR;
using Skillup.Modules.Finances.Core.DTO;

namespace Skillup.Modules.Finances.Core.Features.Requests.Queries
{
    public record GetMonthlyEarningsPerCoursesByAuthorIdRequest(Guid AuthorId, int Year) : IRequest<YearEarningsDto>;
}
