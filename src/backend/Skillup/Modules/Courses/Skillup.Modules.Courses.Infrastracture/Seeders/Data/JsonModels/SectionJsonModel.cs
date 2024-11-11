namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels
{
    internal class SectionJsonModel
    {
        public string Title { get; set; }
        public int Index { get; set; }
        public bool IsPublished { get; set; }
        public Guid CourseId { get; set; }
    }
}
