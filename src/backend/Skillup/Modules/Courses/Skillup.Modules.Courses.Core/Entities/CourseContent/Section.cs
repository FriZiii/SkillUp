using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Core.Entities.CourseContent
{
    public class Section
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; } //section is finished when all elemnets of the section are finished

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public List<Element> Elements { get; set; }  //element ex. one movie clip
    }
}
