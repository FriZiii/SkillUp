using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Exceptions;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    public class AddDetailsHandler : IRequestHandler<AddDetailsRequest>
    {
        private readonly ICourseRepository _courseRepository;

        public AddDetailsHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task Handle(AddDetailsRequest request, CancellationToken cancellationToken)
        {
            Uri url;
            try
            {
                url = new Uri(request.ThumbnailUrl) ?? null;
            }
            catch
            {
                throw new InvalidUrlException();
            }
            var details = new CourseDetails()
            {
                Subtitle = request.Subtitle,
                Description = request.Description,
                Level = request.Level,
                ObjectivesSummary = new StringListValueObject(request.ObjectivesSummary),
                MustKnowBefore = new StringListValueObject(request.MustKnowBefore),
                IntendedFor = new StringListValueObject(request.IntendedFor),
                ThumbnailUrl = url ?? null,
            };

            await _courseRepository.AddDetails(request.CoruseId, details);
        }
    }
}
