using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class AddSectionHandler : IRequestHandler<AddSectionRequest>
    {
        private readonly ISectionRepository _sectionRepository;

        public AddSectionHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
        public async Task Handle(AddSectionRequest request, CancellationToken cancellationToken)
        {
            var section = new Section()
            {
                Title = request.Title,
                Index = request.index,
                CourseId = request.CourseId,
            };
            await _sectionRepository.Add(section);

            request.SectionId = section.Id;
        }
    }
}
