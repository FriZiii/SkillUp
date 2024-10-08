using MediatR;
using Skillup.Modules.Courses.Core.Entities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Application.Operations.Commands.AddDetails
{
    public class AddDetailsHandler : IRequestHandler<AddDetails>
    {
        private readonly ICourseRepository _courseRepository;

        public AddDetailsHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task Handle(AddDetails request, CancellationToken cancellationToken)
        {
            var details = new CourseDetails()
            {
                Description = request.Description,
                Level = request.Level,
                ObjectivesSummary = new StringListValueObject(request.ObjectivesSummary),
                MustKnowBefore = new StringListValueObject(request.MustKnowBefore),
                IntendedFor = new StringListValueObject(request.IntendedFor)
            };
            await _courseRepository.AddDetails(request.CoruseId, details);

        }
    }
}
