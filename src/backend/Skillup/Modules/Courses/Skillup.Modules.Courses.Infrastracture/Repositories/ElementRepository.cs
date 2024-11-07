using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class ElementRepository : IElementRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Element> _elements;

        public ElementRepository(CoursesDbContext context)
        {
            _context = context;
            _elements = context.Elements;
        }
        public async Task AddElement(Element element)
        {
            _elements.Add(element);
            await _context.SaveChangesAsync();
        }

        public Task AddAssignment(Element element)
        {
            throw new NotImplementedException();
        }


        public async Task<List<Element>> GetElementsBySectionId(Guid sectionId)
        {
            var elements = await _elements
                .Where(e => e.SectionId == sectionId)
                .OrderBy(x => x.Index)
                .ToListAsync();
            return elements;
        }

        public async Task Edit(Element element)
        {
            var elementToEdit = await _elements.FirstOrDefaultAsync(s => s.Id == element.Id) ?? throw new Exception();  //TODO: Custom exception for null check in repo

            elementToEdit.Title = element.Title;
            elementToEdit.Description = element.Description;
            elementToEdit.Index = element.Index;

            await _context.SaveChangesAsync();
        }

        public async Task<Element> GetById(Guid elementId)
        {
            var element = await _elements.FirstOrDefaultAsync(e => e.Id == elementId) ?? throw new Exception();  //TODO: Custom exception for null check in repo
            return element;
        }

        public async Task Delete(Element element)
        {
            _elements.Remove(element);
            await _context.SaveChangesAsync();
        }
    }
}
