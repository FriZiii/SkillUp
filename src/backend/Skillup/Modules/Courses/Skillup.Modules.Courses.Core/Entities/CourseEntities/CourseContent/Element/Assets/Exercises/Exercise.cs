using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets.Exercises
{
    public abstract class Exercise
    {
        public Guid Id { get; set; }
        public Guid AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
