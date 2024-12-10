using MediatR;
using Skillup.Modules.Courses.Core.DTO.Rating;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetRatingsByUserIdRequest(Guid UserId) : IRequest<IEnumerable<CourseUserRatingDto>>;
}
