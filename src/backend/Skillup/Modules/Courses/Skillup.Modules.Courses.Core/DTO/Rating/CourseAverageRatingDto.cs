namespace Skillup.Modules.Courses.Core.DTO.Rating
{
    public class CourseAverageRatingDto : CourseRatingDto
    {
        public int AverageStars { get; set; }
        public int RatingsCount { get; set; }
    }
}
