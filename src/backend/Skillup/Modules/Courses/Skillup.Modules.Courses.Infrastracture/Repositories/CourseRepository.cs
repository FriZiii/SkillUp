using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class CourseRepository : ICourseRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Course> _courses;

        public CourseRepository(CoursesDbContext context)
        {
            _context = context;
            _courses = context.Courses;
        }

        public async Task Add(Course course)
        {
            await _courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _courses.ToListAsync();
        }
    }
}
