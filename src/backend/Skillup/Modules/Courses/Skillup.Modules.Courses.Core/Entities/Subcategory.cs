namespace Skillup.Modules.Courses.Core.Entities
{
    public class Subcategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Course> Courses { get; set; }
        //połączenie z kategorią zastanowic się jak bedzie najlepiej na frontendzie
    }
}
