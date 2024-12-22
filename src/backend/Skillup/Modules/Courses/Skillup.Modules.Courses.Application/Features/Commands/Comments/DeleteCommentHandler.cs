using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Comments;

namespace Skillup.Modules.Courses.Application.Features.Commands.Comments
{
    internal class DeleteCommentHandler(ICommentRepository commentRepository) : IRequestHandler<DeleteCommentRequest>
    {
        private readonly ICommentRepository commentRepository = commentRepository;

        public async Task Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
        {
            await commentRepository.Delete(request.CommentId);
        }
    }
}
