namespace Skillup.Modules.Courses.Core.DTO
{
    public class SectionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Index { get; set; }
        public List<ElementDto> Elements { get; set; }
    }
}
