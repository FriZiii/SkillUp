namespace Skillup.Modules.Courses.Core.Entities.CourseContent.Assets.Exercises
{
    public class QuizQuestion : Exercise
    {
        public string Question { get; set; }
        public List<QuizAnswer> Answers { get; set; }
    }
}
