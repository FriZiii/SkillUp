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
        public async Task Add(Category category)
        {
            await _catogories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _catogories.Include(c => c.Subcategories).ToListAsync();
        }

        public async Task<Category> GetById(Guid id)
        {
            return await _catogories.SingleOrDefaultAsync(c => c.Id == id);
        }
    }
}
