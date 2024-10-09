using Skillup.Modules.Courses.Application.Operations;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Shared.Abstractions.Kernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Courses.Core.DTO
{
    public class CourseDetailDto : CourseDto
    {
        public string Description { get; set; }
        public CourseLevel Level { get; set; }
        public List<string> ObjectivesSummary { get; set; }
        public List<string> MustKnowBefore { get; set; }
        public List<string> IntendedFor { get; set; }
        public List<SectionDto> Sections {  get; set; }
    }
}
