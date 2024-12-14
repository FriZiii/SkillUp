using MediatR;
using Skillup.Modules.Courses.Core.DTO.Rating;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetCourseRatingByIdRequest(Guid Id) : IRequest<CourseUserRatingDto>;
}
