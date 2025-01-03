using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Review;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Courses.Application.Features.Commands.Review
{
    internal class ResolveCommentHandler(IReviewCommentRepository reviewCommentRepository) : IRequestHandler<ResolveCommentRequest>
    {
        private readonly IReviewCommentRepository _reviewCommentRepository = reviewCommentRepository;

        public async Task Handle(ResolveCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = await _reviewCommentRepository.Get(request.CommentId) ?? throw new NotFoundException($"Comment with ID {request.CommentId} not found");
            comment.IsResolved = true;
            request.ReviewId = comment.CourseReviewId;
            await _reviewCommentRepository.Update(comment);
        }
    }
}
