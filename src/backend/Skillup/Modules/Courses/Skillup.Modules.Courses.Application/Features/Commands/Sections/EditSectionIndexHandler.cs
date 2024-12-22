using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Sections;

namespace Skillup.Modules.Courses.Application.Features.Commands.Sections
{
    internal class EditSectionIndexHandler : IRequestHandler<EditSectionIndexRequest, List<SectionDto>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ILogger<EditSectionIndexHandler> _logger;

        public EditSectionIndexHandler(ISectionRepository sectionRepository, ILogger<EditSectionIndexHandler> logger)
        {
            _sectionRepository = sectionRepository;
            _logger = logger;
        }

        public async Task<List<SectionDto>> Handle(EditSectionIndexRequest request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetById(request.SectionId);
            var sections = await _sectionRepository.GetSectionsByCourseId(section.CourseId);
            var oldIndex = section.Index;

            section.Index = request.index;

            await _sectionRepository.Edit(section);
            if (oldIndex < request.index)
            {
                for (int i = oldIndex + 1; i <= request.index; i++)
                {
                    sections[i].Index -= 1;
                }
                await _sectionRepository.EditIndexes(sections);
            }
            else if (oldIndex > request.index)
            {
                for (int i = request.index; i < oldIndex; i++)
                {
                    sections[i].Index += 1;
                }
                await _sectionRepository.EditIndexes(sections);
            }

            var sectionMapper = new SectionMapper();
            var newSections = await _sectionRepository.GetSectionsByCourseId(section.CourseId);
            var sectionDtos = newSections.Select(s => sectionMapper.SectionToSectionDto(s)).ToList();
            _logger.LogInformation("Section index changed");
            return sectionDtos;
        }
    }
}
