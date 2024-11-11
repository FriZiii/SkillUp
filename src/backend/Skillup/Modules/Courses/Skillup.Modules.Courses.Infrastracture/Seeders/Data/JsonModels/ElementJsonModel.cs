namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels
{
    internal class ArticleElementJsonModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Index { get; set; }
        public bool IsFree { get; set; }
        public string SectionTitle { get; set; }
        public ArticleJsonModel Article { get; set; }
    }

    internal class ArticleJsonModel
    {
        public string HTMLContent { get; set; }
    }

    internal class VideoElementJsonModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Index { get; set; }
        public bool IsFree { get; set; }
        public string SectionTitle { get; set; }
        public VideoJsonModel Video { get; set; }
    }

    internal class VideoJsonModel
    {
        public Uri Url { get; set; }
    }

    internal class AssignmentElementJsonModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public int Index { get; set; }
        public bool IsFree { get; set; }
        public string SectionTitle { get; set; }
        public AssignmentJsonModel Assignment { get; set; }
    }

    internal class AssignmentJsonModel
    {
        public string Instruction { get; set; }
    }
}
