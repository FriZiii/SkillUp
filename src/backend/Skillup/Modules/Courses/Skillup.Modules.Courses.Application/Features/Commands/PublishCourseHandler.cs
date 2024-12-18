using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Shared.Abstractions.Events.Notifications;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class PublishCourseHandler(ICourseRepository courseRepository, ILogger<PublishCourseHandler> logger, IPublishEndpoint publishEndpoint) : IRequestHandler<EditCourseStatusRequest>
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly ILogger<PublishCourseHandler> _logger = logger;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(EditCourseStatusRequest request, CancellationToken cancellationToken)
        {
            await _courseRepository.EditCourseStatus(request.CourseId, request.Status);

            await PublishNotification(request.CourseId, request.Status);

            _logger.LogInformation("Course published");
        }

        private async Task PublishNotification(Guid courseId, CourseStatus status)
        {
            var course = await _courseRepository.GetById(courseId);
            if (course == null) return;

            if (status == CourseStatus.ChangesRequired)
            {
                await _publishEndpoint.Publish(new NotificationPublished(NotifitationType.Instructor, course.AuthorId, $"Review of your course {course.Title} has been completed, unfortunately your course does not meet our requirements. You need to make the appropriate changes to your course."));
            }
            else if (status == CourseStatus.Published)
            {
                await _publishEndpoint.Publish(new NotificationPublished(NotifitationType.Instructor, course.AuthorId, $"Congratulations your course {course.Title} met our requirements and was published."));
            }
        }
    }
}
