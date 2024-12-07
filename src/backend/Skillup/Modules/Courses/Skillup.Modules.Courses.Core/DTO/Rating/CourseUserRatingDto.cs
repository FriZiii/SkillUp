namespace Skillup.Modules.Courses.Core.DTO.Rating
{
    public class CourseUserRatingDto : CourseRatingDto
    {
        public Guid Id { get; set; }
        public Guid RatedById { get; set; }
        public int Stars { get; set; }
        public string Feedback { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
