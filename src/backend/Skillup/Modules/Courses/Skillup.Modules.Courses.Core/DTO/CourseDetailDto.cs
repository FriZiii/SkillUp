using Skillup.Modules.Courses.Application.Operations;

namespace Skillup.Modules.Courses.Core.DTO
{
    public class CourseDetailDto : CourseDto
    {
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ObjectivesSummary { get; set; }
        public IEnumerable<string> MustKnowBefore { get; set; }
        public IEnumerable<string> IntendedFor { get; set; }
        public IEnumerable<SectionDto> Sections { get; set; }
    }
}
