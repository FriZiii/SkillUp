using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Review;

namespace Skillup.Modules.Courses.Application.Features.Commands.Review
{
    internal class DeleteReviewCommentHandler(IReviewCommentRepository reviewCommentRepository) : IRequestHandler<DeleteReviewCommentRequest>
    {
        private readonly IReviewCommentRepository _reviewCommentRepository = reviewCommentRepository;
        public async Task Handle(DeleteReviewCommentRequest request, CancellationToken cancellationToken)
        {
            await _reviewCommentRepository.Delete(request.CommentId);
        }
    }
}
