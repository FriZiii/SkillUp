using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO.Rating;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    internal class GetRatingsByCourseIdHandler(ICourseRatingRepository courseRatingRepository, IAmazonS3Service amazonS3Service) : IRequestHandler<GetRatingsByCourseIdRequest, CourseDetailedRatingDto?>
    {
        private readonly ICourseRatingRepository _courseRatingRepository = courseRatingRepository;
        private readonly IAmazonS3Service _amazonS3Service = amazonS3Service;

        public async Task<CourseDetailedRatingDto?> Handle(GetRatingsByCourseIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new CourseRatingMapper();
            var ratings = await _courseRatingRepository.GetByCourseId(request.CourseId);

            if (ratings == null || ratings.Count() == 0)
            {
                return null;
            }


            var userRatings = request.LatestUserRatingsCount == null
              ? ratings.OrderByDescending(x => x.Timestamp)
              : ratings.OrderByDescending(x => x.Timestamp).Take((int)request.LatestUserRatingsCount);

            return new CourseDetailedRatingDto()
            {
                Rating = mapper.CourseRatingToCourseAverageRatingDto(ratings),
                UserRatings = userRatings.Select(x => mapper.CourseRatingToCourseUserRatingDetailedDto(x, _amazonS3Service))
            };
        }
    }
}
