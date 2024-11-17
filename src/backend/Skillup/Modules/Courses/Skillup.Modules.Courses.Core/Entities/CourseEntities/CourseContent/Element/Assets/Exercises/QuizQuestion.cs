namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets.Exercises
{
    public class QuizQuestion : Exercise
    {
        public string Question { get; set; }
        public List<QuizAnswer> Answers { get; set; }
    }
}
