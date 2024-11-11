using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements
{
    public class AddElementHandler : IRequestHandler<AddElementRequest>
    {
        private readonly IElementRepository _elementRepository;

        public AddElementHandler(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }
        public async Task Handle(AddElementRequest request, CancellationToken cancellationToken)
        {
            var element = new Element()
            {
                Title = request.Title,
                Description = request.Description,
                Type = request.Type,
                Index = request.Index,
                SectionId = request.SectionId,
            };
            await _elementRepository.Add(element);
        }
    }
}
