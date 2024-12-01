namespace Skillup.Modules.Courses.Core.DTO.Assets.Exercises
{
    public class QuizQuestionDto : ExerciseDto
    {
        public string Question { get; set; }
        public IEnumerable<QuizAnswerDto> Answers { get; set; }
    }
}
