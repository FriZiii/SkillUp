using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class PublishCourseHandler : IRequestHandler<PublishCourseRequest>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<PublishCourseHandler> _logger;

        public PublishCourseHandler(ICourseRepository courseRepository, ILogger<PublishCourseHandler> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }
        public async Task Handle(PublishCourseRequest request, CancellationToken cancellationToken)
        {
            await _courseRepository.Publish(request.CourseId);
            _logger.LogInformation("Course published");
        }
    }
}
