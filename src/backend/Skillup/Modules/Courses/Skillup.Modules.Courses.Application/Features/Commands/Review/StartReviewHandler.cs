using MassTransit;
using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Review;
using Skillup.Shared.Abstractions.Events.Notifications;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Application.Features.Commands.Review
{
    internal class StartReviewHandler(ICourseReviewRepository courseReviewRepository, IMediator mediator, IClock clock, IPublishEndpoint publishEndpoint, ICourseRepository courseRepository) : IRequestHandler<StartReviewRequest, CourseReview>
    {
        private readonly ICourseReviewRepository _courseReviewRepository = courseReviewRepository;
        private readonly IMediator _mediator = mediator;
        private readonly IClock _clock = clock;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly ICourseRepository _courseRepository = courseRepository;

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

            await PublishNotification(request.CourseId);

            return review;
        }

        private async Task PublishNotification(Guid courseId)
        {
            var course = await _courseRepository.GetById(courseId);
            if (course == null) return;
            await _publishEndpoint.Publish(new NotificationPublished(NotifitationType.Instructor, course.AuthorId, $"Review has been started for your course {course.Title}"));
        }
    }
}
