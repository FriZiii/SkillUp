using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Category> _catogories;

        public CategoryRepository(CoursesDbContext context)
        {
            _context = context;
            _catogories = context.Categories;
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _catogories.Include(c => c.Subcategories).OrderBy(x => x.Index).ToListAsync();
        }
    }
}
