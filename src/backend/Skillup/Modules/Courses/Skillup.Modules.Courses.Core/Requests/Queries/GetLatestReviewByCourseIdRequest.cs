using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Requests.Queries
{
    public record GetLatestReviewByCourseIdRequest(Guid CourseId) : IRequest<CourseReview>;
}
