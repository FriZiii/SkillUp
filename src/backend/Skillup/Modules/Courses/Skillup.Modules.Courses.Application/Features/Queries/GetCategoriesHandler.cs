using MediatR;
using Skillup.Modules.Courses.Application.Mappings;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Application.Features.Queries
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, IEnumerable<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        {
            CategoryMapper categoryMapper = new();
            var categories = await _categoryRepository.GetAll();
            var categorisDtos = categories.Select(categoryMapper.CategoryToCategoryDto);
            return categorisDtos;
        }
    }
}
