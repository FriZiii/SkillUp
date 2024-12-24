using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Review
{
    public record AddReviewCommentRequest(Guid ReviewId, Guid CourseId, Guid? ElementId, string Comment) : IRequest;
}
