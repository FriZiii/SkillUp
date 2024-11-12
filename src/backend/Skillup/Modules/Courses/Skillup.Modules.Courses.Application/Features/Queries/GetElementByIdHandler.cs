using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public class GetElementByIdHandler : IRequestHandler<GetElementByIdRequest, ElementDto>
    {
        private readonly IElementRepository _elementRepository;

        public GetElementByIdHandler(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }
        public async Task<ElementDto> Handle(GetElementByIdRequest request, CancellationToken cancellationToken)
        {
            ElementMapper elementMapper = new();
            var element = await _elementRepository.GetById(request.ElementId);
            var elementDto = elementMapper.ElementToElementDto(element);
            return elementDto;
        }
    }
}
