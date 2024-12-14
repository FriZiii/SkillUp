using MediatR;
using Skillup.Modules.Courses.Core.DTO.Review;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetReviewsByCourseIdRequest(Guid CourseId) : IRequest<IEnumerable<CourseReviewDto>>;
}
