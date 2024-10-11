namespace Skillup.Modules.Courses.Core.DTO
{
    public class ElementDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsFree { get; set; }
        public bool IsPublished { get; set; }
    }
}
