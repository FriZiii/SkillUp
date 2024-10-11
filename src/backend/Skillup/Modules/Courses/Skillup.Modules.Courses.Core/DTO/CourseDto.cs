using Skillup.Modules.Courses.Core.DTO;

namespace Skillup.Modules.Courses.Application.Operations
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        //author
        public CategoryDto Category { get; set; }
        public Uri ThumbnailUrl { get; set; }
    }
}
