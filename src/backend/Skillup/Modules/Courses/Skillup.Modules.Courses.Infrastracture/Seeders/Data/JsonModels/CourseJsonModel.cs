namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels
{
    internal class CourseJsonModel
    {
        public Guid AuthorId { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public string Status { get; set; }
        public CourseDetailsJsonModel Details { get; set; }
        public List<SectionJsonModel>? Sections { get; set; }
        public Guid Id { get; set; }
    }
}
