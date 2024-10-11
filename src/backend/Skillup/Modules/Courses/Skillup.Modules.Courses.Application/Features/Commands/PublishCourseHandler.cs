using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class PublishCourseHandler : IRequestHandler<PublishCourseRequest>
    {
        private readonly ICourseRepository _courseRepository;

        public PublishCourseHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task Handle(PublishCourseRequest request, CancellationToken cancellationToken)
        {
            await _courseRepository.Publish(request.CourseId);
        }
    }
}
