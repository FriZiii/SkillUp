namespace Skillup.Modules.Courses.Infrastracture.Seeders.Data.JsonModels
{
    internal class QuizJsonModel
    {
        public string Question { get; set; }
        public string ElementTitle { get; set; }
        public List<QuizAnswerJsonModel> Answers { get; set; }
    }

    internal class QuizAnswerJsonModel
    {
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

    }
}
