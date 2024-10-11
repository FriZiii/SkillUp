using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class SectionMapper
    {
        ElementMapper _elementMapper = new();
        public SectionDto SectionToSectionDto(Section section)
        {
            var sectionDto = new SectionDto()
            {
                Id = section.Id,
                Title = section.Title,
                Elements = section.Elements.Select(_elementMapper.ElementToElementDto).ToList()
            };
            return sectionDto;
        }
    }
}
