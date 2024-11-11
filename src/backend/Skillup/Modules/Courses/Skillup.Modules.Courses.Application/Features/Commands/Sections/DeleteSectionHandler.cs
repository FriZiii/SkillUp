using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Sections;

namespace Skillup.Modules.Courses.Application.Features.Commands.Sections
{
    internal class DeleteSectionHandler : IRequestHandler<DeleteSectionRequest>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ILogger<DeleteSectionHandler> _logger;

        public DeleteSectionHandler(ISectionRepository sectionRepository, ILogger<DeleteSectionHandler> logger)
        {
            _sectionRepository = sectionRepository;
            _logger = logger;
        }
        public async Task Handle(DeleteSectionRequest request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetById(request.SectionId);
            await _sectionRepository.Delete(request.SectionId);
            _logger.LogInformation("Section deleted");

            var sections = await _sectionRepository.GetSectionsByCourseId(section.CourseId);
            for (int i = 0; i < sections.Count(); i++)
            {
                sections[i].Index = i;
            }
            await _sectionRepository.EditIndexes(sections);
        }
    }
}
