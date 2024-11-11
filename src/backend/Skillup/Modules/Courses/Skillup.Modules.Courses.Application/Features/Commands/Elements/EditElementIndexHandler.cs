using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements
{
    internal class EditElementIndexHandler : IRequestHandler<EditElementIndexRequest, SectionDto>
    {
        private readonly IElementRepository _elementRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly ILogger<EditElementIndexHandler> _logger;

        public EditElementIndexHandler(IElementRepository elementRepository, ISectionRepository sectionRepository, ILogger<EditElementIndexHandler> logger)
        {
            _elementRepository = elementRepository;
            _sectionRepository = sectionRepository;
            _logger = logger;
        }
        public async Task<SectionDto> Handle(EditElementIndexRequest request, CancellationToken cancellationToken)
        {
            var element = await _elementRepository.GetById(request.ElementId);
            var elements = await _elementRepository.GetElementsBySectionId(element.SectionId);
            var oldIndex = element.Index;

            element.Index = request.index;
            await _elementRepository.Edit(element);
            if (oldIndex < request.index)
            {
                for (int i = oldIndex + 1; i <= request.index; i++)
                {
                    elements[i].Index -= 1;
                }
                await _elementRepository.EditIndexes(elements);
            }
            else if (oldIndex > request.index)
            {
                for (int i = request.index; i < oldIndex; i++)
                {
                    elements[i].Index += 1;
                }
                await _elementRepository.EditIndexes(elements);
            }

            var sectionMapper = new SectionMapper();
            var newSection = await _sectionRepository.GetById(element.SectionId);
            newSection.Elements = newSection.Elements.OrderBy(e => e.Index).ToList();
            var sectionDto = sectionMapper.SectionToSectionDto(newSection);
            _logger.LogInformation("Element index edited");
            return sectionDto;
        }
    }
}
