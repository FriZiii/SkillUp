using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Application.Operations
{
    public class CourseDto
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        //author
        public CategoryDto Category { get; set; }
        public Uri ThumbnailUrl { get; set; }
    }
}
