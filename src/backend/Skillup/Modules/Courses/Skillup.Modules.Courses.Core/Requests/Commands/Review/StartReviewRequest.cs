using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Review
{
    public record StartReviewRequest(Guid CourseId) : IRequest<CourseReview>
    {
        public Guid ReviewId { get; set; } = Guid.NewGuid();
    }
}
