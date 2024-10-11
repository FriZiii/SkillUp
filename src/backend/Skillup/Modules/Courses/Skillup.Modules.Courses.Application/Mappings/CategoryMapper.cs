﻿using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class CategoryMapper
    {
        SubcategoryMapper subcategoryMapper = new SubcategoryMapper();
        public CategoryDto CategoryToCategoryDto(Category category)
        {
            var categoryDto = new CategoryDto()
            {
                Id = category.Id,
                Name = category.Name,
                Subcategories = category.Subcategories.Select(s => subcategoryMapper.SubcategoryToSubcategoryDto(s))
            };
            return categoryDto;
        }
    }
}
