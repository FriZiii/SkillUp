namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data
{
    internal class CourseDetailsJsonModel
    {
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public List<string> ObjectivesSummary { get; set; }
        public List<string> MustKnowBefore { get; set; }
        public List<string> IntendedFor { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}