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
            await _sectionRepository.Delete(request.SectionId);
            _logger.LogInformation("Section deleted");
        }
    }
}
