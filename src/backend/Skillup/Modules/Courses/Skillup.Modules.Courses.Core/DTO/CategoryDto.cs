namespace Skillup.Modules.Courses.Core.DTO
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<SubcategoryDto> Subcategories { get; set; }
    }
}
