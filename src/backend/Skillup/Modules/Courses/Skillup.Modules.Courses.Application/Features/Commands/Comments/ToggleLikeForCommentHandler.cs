using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Comments;

namespace Skillup.Modules.Courses.Application.Features.Commands.Comments
{
    internal class ToggleLikeForCommentHandler(ICommentRepository commentRepository) : IRequestHandler<ToggleLikeForCommentRequest>
    {
        private readonly ICommentRepository commentRepository = commentRepository;

        public async Task Handle(ToggleLikeForCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = await commentRepository.Get(request.CommentId);
            request.ElementId = comment.ElementId;
            await commentRepository.ToggleLikeComment(request.CommentId, request.UserId);
        }
    }
}
