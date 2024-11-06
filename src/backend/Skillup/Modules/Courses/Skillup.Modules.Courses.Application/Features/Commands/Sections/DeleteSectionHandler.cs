using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Sections;

namespace Skillup.Modules.Courses.Application.Features.Commands.Sections
{
    internal class DeleteSectionHandler : IRequestHandler<DeleteSectionRequest>
    {
        private readonly ISectionRepository _sectionRepository;

        public DeleteSectionHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
        public async Task Handle(DeleteSectionRequest request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetById(request.SectionId);
            await _sectionRepository.Delete(section);
        }
    }
}
