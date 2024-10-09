using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
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

        public async Task AddDetails(Guid courseId, CourseDetails details)
        {
            var course = await _courses.SingleOrDefaultAsync(c => c.Id == courseId);
            course.Details = details;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _courses
                .Include(c => c.Category)
                .Include(c => c.Subcategory)
                .ToListAsync();
        }

        public async Task<Course> GetById(Guid id)
        {
            var course = await _courses
                .Include(c => c.Category)
                .Include(c => c.Subcategory)
                .Include(c => c.Details)
                .Include(c => c.Sections)
                .FirstOrDefaultAsync(c => c.Id == id);

            return course;
        }
    }
}
