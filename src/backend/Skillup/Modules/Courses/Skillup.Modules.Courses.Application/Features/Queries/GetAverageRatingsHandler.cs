using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Rating;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetAverageRatingsHandler(ICourseRatingRepository courseRatingRepository) : IRequestHandler<GetAverageRatingsRequest, IEnumerable<CourseAverageRatingDto>>
    {
        private readonly ICourseRatingRepository _courseRatingRepository = courseRatingRepository;
        public async Task<IEnumerable<CourseAverageRatingDto>> Handle(GetAverageRatingsRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseRatingMapper();
            var ratings = await _courseRatingRepository.Get();
            var rattigsByCourse = ratings.GroupBy(x => x.CourseId);
            return rattigsByCourse.Select(mapper.CourseRatingToCourseAverageRatingDto);
        }
    }
}
