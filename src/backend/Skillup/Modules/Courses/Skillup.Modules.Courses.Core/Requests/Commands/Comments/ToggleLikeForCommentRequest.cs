using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Comments
{
    public record ToggleLikeForCommentRequest(Guid CommentId, Guid UserId) : IRequest;
}
