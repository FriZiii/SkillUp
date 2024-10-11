using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IElementRepository
    {
        Task AddElement(Element element); 
        Task AddAssignment(Element element);
    }
}
