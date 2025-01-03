using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

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
                .Include(s => s.Elements).ThenInclude(e => e.Asset)
                .FirstOrDefaultAsync(s => s.Id == sectionId) ?? throw new BadRequestException($"Section with ID {sectionId} doesn't exist");
            return section;
        }

        public async Task<List<Section>> GetSectionsByCourseId(Guid courseId)
        {
            var sections = await _sections
                .Include(s => s.Elements).ThenInclude(e => e.Asset)
                .Include(s => s.Elements).ThenInclude(e => e.Attachments)
                .Where(s => s.CourseId == courseId)
                .OrderBy(x => x.Index)
                .ToListAsync();
            return sections;
        }

        public async Task Edit(Section section)
        {
            var sectionToEdit = await _sections.FirstOrDefaultAsync(s => s.Id == section.Id) ?? throw new BadRequestException($"Section with ID {section.Id} doesn't exist");

            sectionToEdit.Title = section.Title;
            sectionToEdit.IsPublished = section.IsPublished;
            sectionToEdit.Index = section.Index;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid sectionId)
        {
            var section = _sections.FirstOrDefault(e => e.Id == sectionId) ?? throw new Exception();
            _sections.Remove(section);

            var sectionsToChange = _sections.Where(s => s.CourseId == section.CourseId && s.Id != section.Id).OrderBy(x => x.Index);
            for (int i = 0; i < sectionsToChange.Count(); i++)
            {
                sectionsToChange.ElementAt(i).Index = i;
            }
            await _context.SaveChangesAsync();
        }

        public async Task EditIndexes(IEnumerable<Section> sections)
        {
            foreach (var section in _sections.Where(s => sections.Select(se => se.Id).Contains(s.Id)))
            {
                section.Index = sections.First(el => el.Id == section.Id).Index;
            }
            await _context.SaveChangesAsync();
        }
    }
}