using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements
{
    internal class EditElementHandler : IRequestHandler<EditElementRequest>
    {
        private readonly IElementRepository _elementRepository;

        public EditElementHandler(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }
        public async Task Handle(EditElementRequest request, CancellationToken cancellationToken)
        {
            var element = await _elementRepository.GetById(request.ElementId);
            element.Title = request.Title;
            element.Description = request.Description;
            await _elementRepository.Edit(element);
        }
    }
}
