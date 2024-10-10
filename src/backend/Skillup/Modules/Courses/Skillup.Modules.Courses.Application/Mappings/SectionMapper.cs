using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class SectionMapper
    {
        ElementMapper elementMapper = new ElementMapper();
        public SectionDto SectionToSectionDto(Section section)
        {
            var sectionDto = new SectionDto()
            {
                Id = section.Id,
                Title = section.Title,
                Elements = section.Elements.Select(e => elementMapper.ElementToElementDto(e)).ToList()
            };
            return sectionDto;
        }
    }
}
