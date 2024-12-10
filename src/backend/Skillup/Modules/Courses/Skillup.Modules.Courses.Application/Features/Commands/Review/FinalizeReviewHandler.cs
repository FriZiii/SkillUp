using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Modules.Courses.Core.Requests.Commands.Review;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Application.Features.Commands.Review
{
    internal class FinalizeReviewHandler(ICourseReviewRepository courseReviewRepository, IClock clock, IMediator mediator) : IRequestHandler<FinalizeReviewRequest>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;
        private readonly IClock _clock = clock;
        private readonly IMediator _mediator = mediator;

        public async Task Handle(FinalizeReviewRequest request, CancellationToken cancellationToken)
        {
            if (request.Status == ReviewStatus.InProgress)
                return;

            var review = await _courseReviewRepository.Get(request.ReviewId) ?? throw new Exception(); //TODO: Custom ex

            review.Status = request.Status;
            review.FinalizedAt = _clock.CurrentDate();

            await _courseReviewRepository.Update(review);

            if (request.Status == ReviewStatus.Finalized)
            {
                await _mediator.Send(new EditCourseStatusRequest(review.CourseId, CourseStatus.Published));
            }
            else if (request.Status == ReviewStatus.FinalizedWithRequiredChanges)
            {
                await _mediator.Send(new EditCourseStatusRequest(review.CourseId, CourseStatus.ChangesRequired));
            }
        }
    }
}
