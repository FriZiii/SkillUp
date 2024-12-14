namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises
{
    public class QuizAnswer
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public bool IsCorrectAnswer { get; set; }

        public Guid QuestionId { get; set; }
        public QuizQuestion Question { get; set; }
    }
}
