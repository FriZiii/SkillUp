using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Review
{
    public record DeleteReviewCommentRequest(Guid CommentId) : IRequest;
}
