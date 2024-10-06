using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class CourseRepository : ICourseRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Course> _courses;
        private readonly DbSet<Category> _categories;
        private readonly DbSet<Subcategory> _subcategories;

        public CourseRepository(CoursesDbContext context)
        {
            _context = context;
            _courses = context.Courses;
            _categories = context.Categories;
            _subcategories = context.Subcategories;
        }

        public async Task Add(Course course)
        {
            var c = course.Category;

            var s = course.Subcategory;

            await _categories.AddAsync(c);
            await _context.SaveChangesAsync();

            await _subcategories.AddAsync(s);
            await _context.SaveChangesAsync();

            course.Subcategory = s;
            course.Category = c;

            await _courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _courses.ToListAsync();
        }
    }
}
