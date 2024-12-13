using MediatR;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Ratings;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Courses.Application.Features.Commands.Ratings
{
    internal class EditCourseRatingHandler(ICourseRatingRepository courseRatingRepository, IClock clock) : IRequestHandler<EditCourseRatingRequest>
    {
        private readonly ICourseRatingRepository _courseRatingRepository = courseRatingRepository;
        private readonly IClock _clock = clock;

        public async Task Handle(EditCourseRatingRequest request, CancellationToken cancellationToken)
        {
            var rating = await _courseRatingRepository.GetById(request.RatingId) ?? throw new Exception(); //TODO: Custom ex: rating with id doesnt exist
            rating.Stars = request.Stars;
            rating.Feedback = request.Feedback;
            rating.Timestamp = _clock.CurrentDate();

            await _courseRatingRepository.Update(rating);
        }
    }
}
