using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Subcategory> _subcategories;

        public SubcategoryRepository(CoursesDbContext context)
        {
            _context = context;
            _subcategories = context.Subcategories;
        }

        public async Task Add(Subcategory subcategory)
        {
            await _subcategories.AddAsync(subcategory);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Subcategory>> GetAll()
        {
            return await _subcategories.ToListAsync();
        }

        public async Task<Subcategory> GetById(Guid id)
        {
            return await _subcategories.SingleOrDefaultAsync(s => s.Id == id);
        }
    }
}
