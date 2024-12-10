using MediatR;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Ratings;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Application.Features.Commands.Ratings
{
    internal class AddCourseRatingHandler(ICourseRatingRepository courseRatingRepository, IClock clock) : IRequestHandler<AddCourseRatingRequest>
    {
        private readonly ICourseRatingRepository _courseRatingRepository = courseRatingRepository;
        private readonly IClock _clock = clock;

        public async Task Handle(AddCourseRatingRequest request, CancellationToken cancellationToken)
        {
            var courseRatings = new CourseRating()
            {
                RatedById = request.UserId,
                Id = request.RatingId,
                Stars = request.Stars,
                Feedback = request.Feedback,
                CourseId = request.CourseId,
                Timestamp = _clock.CurrentDate()
            };

            await _courseRatingRepository.Add(courseRatings);
        }
    }
}
