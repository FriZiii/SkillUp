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

        public async Task EditDetails(Guid courseId, CourseDetails details)
        {
            var course = await _courses.FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new Exception();  //TODO: course with id doesnt exist
            course.Details = details;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetByStatus(CourseStatus status)
        {
            return await _courses
                   .Where(c => c.Status == status)
                   .Include(c => c.Category)
                   .Include(c => c.Subcategory)
                   .ToListAsync();

        }


        public async Task<Course?> GetById(Guid id)
            => await _courses
                .Include(c => c.Category)
                .Include(c => c.Subcategory)
                .Include(c => c.Sections)
                    .ThenInclude(s => s.Elements)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task EditCourseStatus(Guid courseId, CourseStatus status)
        {
            var course = await _courses.FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new Exception();  //TODO: course with id doesnt exist 
            course.Status = status;
            await _context.SaveChangesAsync();
        }

        public async Task Edit(Course course)
        {
            var courseToEdit = await _courses.FirstOrDefaultAsync(s => s.Id == course.Id) ?? throw new Exception();  //TODO: course with id doesnt exist 

            courseToEdit.Title = course.Title;
            courseToEdit.CategoryId = course.CategoryId;
            courseToEdit.SubcategoryId = course.SubcategoryId;

            await _context.SaveChangesAsync();
        }

        public async Task<int> GetElementsCount(Guid courseId)
        {
            var course = await _courses
                .Include(c => c.Sections)
                .ThenInclude(s => s.Elements)
                .FirstOrDefaultAsync(c => c.Id == courseId) ?? throw new Exception("Course with the given ID does not exist.");

            return course.Sections.Sum(section => section.Elements.Count);
        }
        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _courses
                 .Include(c => c.Category)
                 .Include(c => c.Subcategory)
                 .ToListAsync();
        }

    }
