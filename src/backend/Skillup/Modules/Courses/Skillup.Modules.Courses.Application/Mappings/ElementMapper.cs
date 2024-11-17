using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element;

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
                Description = element.Description,
                Type = element.AssetType,
                Index = element.Index,
                IsFree = element.IsFree,
                HasAsset = element.Asset != null ? true : false,
            };
            return elementeDto;
        }
    }
}
