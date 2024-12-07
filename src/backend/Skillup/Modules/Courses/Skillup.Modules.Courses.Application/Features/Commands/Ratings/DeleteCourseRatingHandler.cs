using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Ratings;

namespace Skillup.Modules.Courses.Application.Features.Commands.Ratings
{
    internal class DeleteCourseRatingHandler(ICourseRatingRepository courseRatingRepository) : IRequestHandler<DeleteCourseRatingRequest>
    {
        private readonly ICourseRatingRepository _courseRatingRepository = courseRatingRepository;
        public async Task Handle(DeleteCourseRatingRequest request, CancellationToken cancellationToken)
        {
            await _courseRatingRepository.Delete(request.Id);
        }
    }
}
