using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Comments
{
    public record DeleteCommentRequest(Guid CommentId) : IRequest;
}
