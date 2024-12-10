using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class PublishCourseHandler(ICourseRepository courseRepository, ILogger<PublishCourseHandler> logger) : IRequestHandler<EditCourseStatusRequest>
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly ILogger<PublishCourseHandler> _logger = logger;

        public async Task Handle(EditCourseStatusRequest request, CancellationToken cancellationToken)
        {
            await _courseRepository.EditCourseStatus(request.CourseId, request.Status);

            //TODO: Send notification about course status change

            _logger.LogInformation("Course published");
        }
    }
}
