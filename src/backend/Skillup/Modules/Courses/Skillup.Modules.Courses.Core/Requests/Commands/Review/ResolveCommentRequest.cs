

using MediatR;

namespace Skillup.Modules.Courses.Core.Requests.Commands.Review
{
    public record ResolveCommentRequest(Guid CommentId) : IRequest
    {
        public Guid ReviewId { get; set; }
    }
}
