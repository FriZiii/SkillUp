namespace Skillup.Modules.Courses.Core.DTO.Rating
{
    public class CourseDetailedRatingDto
    {
        public CourseAverageRatingDto Rating { get; set; }
        public IEnumerable<CourseUserRatingDetailedDto> UserRatings { get; set; }
    }
}
