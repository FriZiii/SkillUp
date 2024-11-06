using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements
{
    internal class EditElementIndexHandler : IRequestHandler<EditElementIndexRequest, List<ElementDto>>
    {
        private readonly IElementRepository _elementRepository;

        public EditElementIndexHandler(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }
        public async Task<List<ElementDto>> Handle(EditElementIndexRequest request, CancellationToken cancellationToken)
        {
            var element = await _elementRepository.GetById(request.ElementId);
            var elements = await _elementRepository.GetElementsBySectionId(element.SectionId);
            var oldIndex = element.Index;

            element.Index = request.index;
            await _elementRepository.Edit(element);
            if (oldIndex < request.index)
            {
                for (int i = oldIndex; i < request.index; i++)
                {
                    elements[i].Index -= 1;
                    await _elementRepository.Edit(elements[i]);
                }
            }
            else if (oldIndex > request.index)
            {
                for (int i = request.index - 1; i < oldIndex - 1; i++)
                {
                    elements[i].Index += 1;
                    await _elementRepository.Edit(elements[i]);
                }
            }

            var elementMapper = new ElementMapper();
            var newElements = await _elementRepository.GetElementsBySectionId(element.SectionId);
            var elementDtos = newElements.Select(elementMapper.ElementToElementDto).ToList();
            return elementDtos;
        }
    }
}
