using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;

namespace Skillup.Modules.Courses.Application.Features.Commands.Elements
{
    internal class DeleteElementHandler : IRequestHandler<DeleteElementRequest>
    {
        private readonly IElementRepository _elementRepository;

        public DeleteElementHandler(IElementRepository elementRepository)
        {
            _elementRepository = elementRepository;
        }
        public async Task Handle(DeleteElementRequest request, CancellationToken cancellationToken)
        {
            var element = await _elementRepository.GetById(request.ElementId);
            await _elementRepository.Delete(element);
        }
    }
}
