using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseContent;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Application.Operations.Commands.AddSection
{
    public class AddSectionHandler : IRequestHandler<AddSection>
    {
        private readonly ISectionRepository _sectionRepository;

        public AddSectionHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
        public async Task Handle(AddSection request, CancellationToken cancellationToken)
        {
            var section = new Section()
            {
                Title = request.Title,
                CourseId = request.CourseId,
            };
            await _sectionRepository.Add(section);

        }
    }
}
