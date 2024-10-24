namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class Subcategory
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
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }
        public List<Course> Courses { get; set; }
    }
}
