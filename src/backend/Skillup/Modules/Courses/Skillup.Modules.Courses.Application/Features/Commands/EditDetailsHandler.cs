using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class EditDetailsHandler : IRequestHandler<EditDetailsRequest>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<EditDetailsHandler> _logger;

        public EditDetailsHandler(ICourseRepository courseRepository, ILogger<EditDetailsHandler> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }
        public async Task Handle(EditDetailsRequest request, CancellationToken cancellationToken)
        {
            var details = new CourseDetails()
            {
                Subtitle = request.Subtitle,
                Description = request.Description,
                Level = request.Level,
                ObjectivesSummary = new StringListValueObject(request.ObjectivesSummary),
                MustKnowBefore = new StringListValueObject(request.MustKnowBefore),
                IntendedFor = new StringListValueObject(request.IntendedFor),
            };

            await _courseRepository.EditDetails(request.CourseId, details);
            _logger.LogInformation("Coure details edited");
        }
    }
}
