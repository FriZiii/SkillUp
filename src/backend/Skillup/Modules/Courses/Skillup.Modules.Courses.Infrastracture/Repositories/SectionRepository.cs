using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
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

        public async Task<Section> GetById(Guid sectionId)
        {
            var section = await _sections
                .Include(s => s.Elements)
                .FirstOrDefaultAsync(s => s.Id == sectionId);
            return section;
        }

        public async Task<List<Section>> GetSectionsByCourseId(Guid courseId)
        {
            var sections = await _sections
                .Include(s => s.Elements)
                .Where(s => s.CourseId == courseId)
                .OrderBy(x => x.Index)
                .ToListAsync();
            return sections;
        }
    }
}
