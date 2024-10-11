using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class ElementMapper
    {
        public ElementDto ElementToElementDto(Element element)
        {
            var elementeDto = new ElementDto()
            {
                Id = element.Id,
                Title = element.Title,
                IsFree = element.IsFree,
                IsPublished = element.IsPublished,
                //Asset
            };
            return elementeDto;
        }
    }
}
