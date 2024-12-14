using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Review;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Application.Features.Commands.Review
{
    internal class AddReviewCommentHandler(IReviewCommentRepository reviewCommentRepository, IClock clock) : IRequestHandler<AddReviewCommentRequest>
    {
        private readonly IReviewCommentRepository _reviewCommentRepository = reviewCommentRepository;
        private readonly IClock _clock = clock;

        public async Task Handle(AddReviewCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = new CourseReviewComment()
            {
                CommentText = request.Comment,
                CourseReviewId = request.ReviewId,
                IsResolved = false,
                CourseElementId = request.ElementId,
                CreatedAt = _clock.CurrentDate()
            };

            await _reviewCommentRepository.Add(comment);
        }
    }
}
