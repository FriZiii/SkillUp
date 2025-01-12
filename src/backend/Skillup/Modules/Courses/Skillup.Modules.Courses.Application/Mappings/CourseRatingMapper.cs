using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO.Rating;
using Skillup.Modules.Courses.Core.DTO.User;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    internal partial class CourseRatingMapper
    {
        public CourseAverageRatingDto CourseRatingToCourseAverageRatingDto(IEnumerable<CourseRating> coursesRatings)
        {
            return new CourseAverageRatingDto()
            {
                RatingsCount = coursesRatings.Count(),
                AverageStars = (int)Math.Round(coursesRatings.Average(x => x.Stars)),
                CourseId = coursesRatings.First().CourseId,
            };
        }

        public CourseUserRatingDetailedDto CourseRatingToCourseUserRatingDetailedDto(CourseRating coursesRating, IAmazonS3Service amazonS3Service, ILogger logger)
        {
            var userMapper = new UserMapper(amazonS3Service, logger);

            return new CourseUserRatingDetailedDto()
            {
                Id = coursesRating.Id,
                CourseId = coursesRating.CourseId,
                RatedBy = (BasicUserDto)userMapper.UserToUserDto(coursesRating.RatedBy, false),
                Stars = coursesRating.Stars,
                Feedback = coursesRating.Feedback,
                Timestamp = coursesRating.Timestamp
            };
        }

        public CourseUserRatingDto CourseRatingToCourseUserRatingDto(CourseRating coursesRating)
        {
            return new CourseUserRatingDto()
            {
                Id = coursesRating.Id,
                CourseId = coursesRating.CourseId,
                RatedById = coursesRating.RatedById,
                Stars = coursesRating.Stars,
                Feedback = coursesRating.Feedback,
                Timestamp = coursesRating.Timestamp
            };
        }
    }
}
