using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Sections;

namespace Skillup.Modules.Courses.Application.Features.Commands.Sections
{
    public class AddSectionHandler : IRequestHandler<AddSectionRequest>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ILogger<AddSectionHandler> _logger;

        public AddSectionHandler(ISectionRepository sectionRepository, ILogger<AddSectionHandler> logger)
        {
            _sectionRepository = sectionRepository;
            _logger = logger;
        }
        public async Task Handle(AddSectionRequest request, CancellationToken cancellationToken)
        {
            var section = new Section()
            {
                Title = request.Title,
                Index = request.index,
                CourseId = request.CourseId,
            };
            await _sectionRepository.Add(section);

            request.SectionId = section.Id;
            _logger.LogInformation("Section added");
        }
    }
}
