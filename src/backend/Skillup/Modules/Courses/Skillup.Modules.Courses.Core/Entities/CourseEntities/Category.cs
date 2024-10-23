namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Subcategory> Subcategories { get; set; }
    }
}

