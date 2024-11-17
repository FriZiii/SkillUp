using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements
{
    public class AddElementHandler : IRequestHandler<AddElementRequest>
    {
        private readonly IElementRepository _elementRepository;
        private readonly ILogger<AddElementHandler> _logger;

        public AddElementHandler(IElementRepository elementRepository, ILogger<AddElementHandler> logger)
        {
            _elementRepository = elementRepository;
            _logger = logger;
        }
        public async Task Handle(AddElementRequest request, CancellationToken cancellationToken)
        {
            var element = new Element()
            {
                Title = request.Title,
                Description = request.Description,
                AssetType = request.AssetType,
                Index = request.Index,
                SectionId = request.SectionId,
            };
            await _elementRepository.Add(element);

            request.ElementId = element.Id;
            _logger.LogInformation("Element added");
        }
    }
}
