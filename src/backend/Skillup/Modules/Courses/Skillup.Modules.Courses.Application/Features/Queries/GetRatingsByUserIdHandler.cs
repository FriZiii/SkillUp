using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Rating;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetRatingsByUserIdHandler(ICourseRatingRepository courseRatingRepository) : IRequestHandler<GetRatingsByUserIdRequest, IEnumerable<CourseUserRatingDto>>
    {
        private readonly ICourseRatingRepository _courseRatingRepository = courseRatingRepository;

        public async Task<IEnumerable<CourseUserRatingDto>> Handle(GetRatingsByUserIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseRatingMapper();
            var ratings = await _courseRatingRepository.GetByUserId(request.UserId);
            return ratings.Select(mapper.CourseRatingToCourseUserRatingDto);
        }
    }
}
