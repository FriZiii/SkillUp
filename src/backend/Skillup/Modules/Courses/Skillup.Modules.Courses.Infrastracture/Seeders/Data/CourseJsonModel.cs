namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data
{
    internal class CourseJsonModel
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public CourseDetailsJsonModel Details { get; set; }
        public List<SectionJsonModel>? Sections { get; set; }
    }
}
