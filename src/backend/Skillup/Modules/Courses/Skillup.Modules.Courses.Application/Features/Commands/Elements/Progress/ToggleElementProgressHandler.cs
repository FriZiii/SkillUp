using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements.Progress;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements.Progress
{
    internal class ToggleElementProgressHandler(ICourseUserProgressRepository courseUserProgressRepository) : IRequestHandler<ToggleCourseElementProgressRequest>
    {
        private readonly ICourseUserProgressRepository _courseUserProgressRepository = courseUserProgressRepository;

        public async Task Handle(ToggleCourseElementProgressRequest request, CancellationToken cancellationToken)
        {
            var progress = await _courseUserProgressRepository.GetByUserAndElementId(request.UserId, request.ElementId);

            if (progress is null)
            {
                await _courseUserProgressRepository.Add(new CourseUserProgess() { CourseId = request.CourseId, ElementId = request.ElementId, UserId = request.UserId });
            }
            else
            {
                await _courseUserProgressRepository.Delete(progress.Id);
            }
        }
    }
}
