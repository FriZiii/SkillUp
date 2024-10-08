using Skillup.Modules.Courses.Core.Entities;

namespace Skillup.Modules.Courses.Application.Operations
{
    public class CourseDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        //author
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }
        public Uri ThumbnailUrl { get; set; }
    }
}
