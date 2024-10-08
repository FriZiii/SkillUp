using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseContent;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class SectionRepository : ISectionRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Section> _sections;

        public SectionRepository(CoursesDbContext context)
        {
            _context = context;
            _sections = context.Sections;
        }

        public async Task Add(Section section)
        {
            _sections.Add(section);
            await _context.SaveChangesAsync();
        }
    }
}
