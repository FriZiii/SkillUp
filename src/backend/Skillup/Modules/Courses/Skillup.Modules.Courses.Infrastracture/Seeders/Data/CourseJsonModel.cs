namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data
{
    internal class CourseJsonModel
    {
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public CourseDetailsJsonModel Details { get; set; }
    }
}
