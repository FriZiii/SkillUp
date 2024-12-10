using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Modules.Courses.Core.Requests.Commands.Review;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Application.Features.Commands.Review
{
    internal class StartReviewHandler(ICourseReviewRepository courseReviewRepository, IMediator mediator, IClock clock) : IRequestHandler<StartReviewRequest, CourseReview>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IClock _clock = clock;

        public async Task<CourseReview> Handle(StartReviewRequest request, CancellationToken cancellationToken)
        {
            var review = new CourseReview()
            {
                Id = request.ReviewId,
                CourseId = request.CourseId,
                CreatedAt = _clock.CurrentDate(),
                Status = ReviewStatus.InProgress,
                FinalizedAt = null,
            };
            await _courseReviewRepository.Add(review);


            await _mediator.Send(new EditCourseStatusRequest(request.CourseId, CourseStatus.PendingReview));
            return review;
        }
    }
}
