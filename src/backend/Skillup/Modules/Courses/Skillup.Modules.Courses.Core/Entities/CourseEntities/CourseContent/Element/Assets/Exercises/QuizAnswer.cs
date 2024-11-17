namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets.Exercises
{
    public class QuizAnswer
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool isCorrectAnswer { get; set; } = false;

        public Guid QuestionId { get; set; }
        public QuizQuestion Question { get; set; }
    }
}
