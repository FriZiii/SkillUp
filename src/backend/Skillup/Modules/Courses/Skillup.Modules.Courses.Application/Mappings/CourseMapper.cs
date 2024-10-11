using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class CourseMapper
    {
        public CourseDto CourseToCourseDto(Course course)
        {
            var courseDto = new CourseDto()
            {
                Id = course.Id,
                Title = course.Title,
                Subtitle = course.Details.Subtitle,
                Category = new CategoryDto()
                {
                    Id = course.Category.Id,
                    Name = course.Category.Name,
                    Subcategory = new SubcategoryDto()
                    {
                        Id = course.Subcategory.Id,
                        Name = course.Subcategory.Name
                    }
                },
                ThumbnailUrl = course.Details.ThumbnailUrl,
            };
            return courseDto;
        }

        public CourseDetailDto CourseToCourseDetailDto(Course course)
        {
            var courseDetailDto = new CourseDetailDto()
            {
                Title = course.Title,
                Subtitle = course.Details.Subtitle,
                Category = new CategoryDto()
                {
                    Id = course.Category.Id,
                    Name = course.Category.Name,
                    Subcategory = new SubcategoryDto()
                    {
                        Id = course.Subcategory.Id,
                        Name = course.Subcategory.Name
                    }
                },
                ThumbnailUrl = course.Details.ThumbnailUrl,
                Description = course.Details.Description,
                Level = course.Details.Level,
                ObjectivesSummary = course.Details.ObjectivesSummary.Values,
                MustKnowBefore = course.Details.MustKnowBefore.Values,
                IntendedFor = course.Details.IntendedFor.Values
            };
            return courseDetailDto;
        }
    }
}
