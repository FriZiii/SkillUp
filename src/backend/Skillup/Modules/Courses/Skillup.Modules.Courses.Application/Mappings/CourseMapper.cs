using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class CourseMapper(IAmazonS3Service s3Service)
    {
        private readonly IAmazonS3Service _s3Service = s3Service;

        public CourseDto CourseToCourseDto(Course course)
        {
            var profilePicture = _s3Service.GetPulicUrl(S3FolderPaths.CourseTubnailPicture + course.Details.ThumbnailKey);

            var courseDto = new CourseDto()
            {
                Id = course.Id,
                Title = course.Title,
                IsPublished = course.IsPublished,
                Category = new CourseCategoryDto()
                {
                    Id = course.Category.Id,
                    Name = course.Category.Name,
                    Slug = course.Category.Slug,
                    Subcategory = new SubcategoryDto()
                    {
                        Id = course.Subcategory.Id,
                        Name = course.Subcategory.Name,
                        Slug = course.Subcategory.Slug
                    }
                },
                ThumbnailUrl = new Uri(profilePicture),
            };
            return courseDto;
        }

        public CourseDetailDto CourseToCourseDetailDto(Course course)
        {
            var profilePicture = _s3Service.GetPulicUrl(S3FolderPaths.CourseTubnailPicture + course.Details.ThumbnailKey);

            var courseDetailDto = new CourseDetailDto()
            {
                Id = course.Id,
                Title = course.Title,
                IsPublished = course.IsPublished,
                Category = new CourseCategoryDto()
                {
                    Id = course.Category.Id,
                    Name = course.Category.Name,
                    Slug = course.Category.Slug,
                    Subcategory = new SubcategoryDto()
                    {
                        Id = course.Subcategory.Id,
                        Name = course.Subcategory.Name,
                        Slug = course.Subcategory.Slug
                    }
                },
                ThumbnailUrl = new Uri(profilePicture),
                Subtitle = course.Details.Subtitle,
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
