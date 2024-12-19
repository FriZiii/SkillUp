using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Application.Operations
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public CourseStatus Status { get; set; }
        public int UsersCout { get; set; }
        public CourseLevel Level { get; set; }
        public CourseCategoryDto Category { get; set; }
        public Uri? ThumbnailUrl { get; set; }
    }
}
