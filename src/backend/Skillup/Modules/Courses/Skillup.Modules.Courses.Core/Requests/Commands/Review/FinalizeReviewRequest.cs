using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Review
{
    public record FinalizeReviewRequest(Guid ReviewId, ReviewStatus Status) : IRequest;
}
