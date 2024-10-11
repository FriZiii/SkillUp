using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class SubcategoryMapper
    {
        public SubcategoryDto SubcategoryToSubcategoryDto(Subcategory subcategory)
        {
            var subcategoryDto = new SubcategoryDto()
            {
                Id = subcategory.Id,
                Name = subcategory.Name
            };
            return subcategoryDto;
        }
    }
}
