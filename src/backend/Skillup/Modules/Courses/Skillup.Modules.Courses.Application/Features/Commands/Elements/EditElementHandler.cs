using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements
{
    internal class EditElementHandler : IRequestHandler<EditElementRequest>
    {
        private readonly IElementRepository _elementRepository;
        private readonly ILogger<EditElementHandler> _logger;

        public EditElementHandler(IElementRepository elementRepository, ILogger<EditElementHandler> logger)
        {
            _elementRepository = elementRepository;
            _logger = logger;
        }
        public async Task Handle(EditElementRequest request, CancellationToken cancellationToken)
        {
            var element = await _elementRepository.GetById(request.ElementId);
            element.Title = request.Title;
            element.Description = request.Description;
            await _elementRepository.Edit(element);
            _logger.LogInformation("Element edited");
        }
    }
}
