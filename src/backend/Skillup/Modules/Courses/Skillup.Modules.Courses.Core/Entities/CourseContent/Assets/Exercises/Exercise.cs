namespace Skillup.Modules.Courses.Core.Entities.CourseContent.Assets.Exercises
{
    public abstract class Exercise
    {
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
