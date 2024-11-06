using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Sections;

namespace Skillup.Modules.Courses.Application.Features.Commands.Sections
{
    internal class EditSectionHandler : IRequestHandler<EditSectionRequest>
    {
        private readonly ISectionRepository _sectionRepository;

        public EditSectionHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
        public async Task Handle(EditSectionRequest request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetById(request.SectionId);
            section.Title = request.Title;
            await _sectionRepository.Edit(section);
        }
    }
}
