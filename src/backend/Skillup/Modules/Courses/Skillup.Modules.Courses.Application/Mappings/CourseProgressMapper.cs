using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    internal partial class CourseProgressMapper
    {
        public async Task<CoursePercentageProgressDto> CourseProgressToPercetageProgressDtoMapper(IEnumerable<CourseUserProgess> courseProgesses, ICourseRepository courseRepository)
        {
            var elementsCount = await courseRepository.GetElementsCount(courseProgesses.First().CourseId);
            var progressCount = courseProgesses.Count();

            return new CoursePercentageProgressDto()
            {
                CourseId = courseProgesses.First().CourseId,
                Percentage = Math.Round(((double)progressCount / (double)elementsCount) * 100, 0)
            };
        }
    }
}
