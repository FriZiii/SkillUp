namespace Skillup.Modules.Courses.Core.DTO.Assets.Exercises
{
    public abstract class ExerciseDto
    {
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
    }
}
