namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class Category
    {
        public Guid Id { get; set; }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                Slug = _name.ToLower().Replace(" ", "-");
            }
        }
        public string Slug { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Subcategory> Subcategories { get; set; }
    }
}

