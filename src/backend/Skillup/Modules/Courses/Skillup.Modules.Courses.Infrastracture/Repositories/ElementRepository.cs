using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
