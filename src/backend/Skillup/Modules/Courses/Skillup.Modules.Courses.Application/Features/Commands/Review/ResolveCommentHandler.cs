using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Review;

namespace Skillup.Modules.Courses.Application.Features.Commands.Review
{
    internal class ResolveCommentHandler(IReviewCommentRepository reviewCommentRepository) : IRequestHandler<ResolveCommentRequest>
    {
        private readonly IReviewCommentRepository _reviewCommentRepository = reviewCommentRepository;

        public async Task Handle(ResolveCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = await _reviewCommentRepository.Get(request.CommentId) ?? throw new Exception(); //TODO: Custom ex: comment with id doesnt exist
            comment.IsResolved = true;
            await _reviewCommentRepository.Update(comment);
        }
    }
}
