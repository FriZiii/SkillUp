using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class CourseMapper
    {
        SectionMapper sectionMapper = new SectionMapper();
        public CourseDto CourseToCourseDto(Course course)
        {
            var courseDto = new CourseDto()
            {
                Title = course.Info.Title,
                Subtitle = course.Info.Subtitle,
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
                ThumbnailUrl = course.ThumbnailUrl,
            };
            return courseDto;
        }

        public CourseDetailDto CourseToCourseDetailDto(Course course)
        {
            var courseDetailDto = new CourseDetailDto()
            {
                Title = course.Info.Title,
                Subtitle = course.Info.Subtitle,
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
                ThumbnailUrl = course.ThumbnailUrl,
                Description = course.Details.Description,
                Level = course.Details.Level,
                ObjectivesSummary = course.Details.ObjectivesSummary.Values,
                MustKnowBefore = course.Details.MustKnowBefore.Values,
                IntendedFor = course.Details.IntendedFor.Values,
                Sections = course.Sections.Select(s => sectionMapper.SectionToSectionDto(s)).ToList(),
            };
            return courseDetailDto;
        }
    }
}
