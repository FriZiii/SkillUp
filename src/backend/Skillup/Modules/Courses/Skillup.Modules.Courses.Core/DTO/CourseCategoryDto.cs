namespace Skillup.Modules.Courses.Core.DTO
{
    public class CourseCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public SubcategoryDto Subcategory { get; set; }
    }
}
