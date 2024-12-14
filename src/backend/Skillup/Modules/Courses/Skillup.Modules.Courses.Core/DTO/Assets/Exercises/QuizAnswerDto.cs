namespace Skillup.Modules.Courses.Core.DTO.Assets.Exercises
{
    public class QuizAnswerDto
    {
        public Guid Id { get; set; }
        public Guid QuizId { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }

    }
}
