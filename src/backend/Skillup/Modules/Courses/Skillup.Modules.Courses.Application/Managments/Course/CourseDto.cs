namespace Skillup.Modules.Courses.Application.Managments.Course
{
    public class CourseDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        //author
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public Uri ThumbnailUrl { get; set; }
    }
}
