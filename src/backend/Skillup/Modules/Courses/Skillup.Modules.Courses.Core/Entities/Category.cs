namespace Skillup.Modules.Courses.Core.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
        public List<Subcategory> Subcategories { get; set; }
    }
}

