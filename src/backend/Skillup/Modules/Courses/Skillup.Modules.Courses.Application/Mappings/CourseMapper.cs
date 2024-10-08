using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Entities;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class CourseMapper
    {
        public CourseDto CourseToCourseDto(Course course)
        {
            var courseDto = new CourseDto()
            {
                Title = course.Info.Title,
                Subtitle = course.Info.Subtitle,
                Category = course.Category,
                Subcategory = null,
                ThumbnailUrl = course.ThumbnailUrl,
            };
            return courseDto;
        }
    }
}
