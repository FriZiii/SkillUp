using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements
{
    internal class DeleteElementHandler : IRequestHandler<DeleteElementRequest>
    {
        private readonly IElementRepository _elementRepository;
        private readonly ILogger<DeleteElementHandler> _logger;

        public DeleteElementHandler(IElementRepository elementRepository, ILogger<DeleteElementHandler> logger)
        {
            _elementRepository = elementRepository;
            _logger = logger;
        }
        public async Task Handle(DeleteElementRequest request, CancellationToken cancellationToken)
        {
            await _elementRepository.Delete(request.ElementId);
            _logger.LogInformation("Element deleted");
        }
    }
}
