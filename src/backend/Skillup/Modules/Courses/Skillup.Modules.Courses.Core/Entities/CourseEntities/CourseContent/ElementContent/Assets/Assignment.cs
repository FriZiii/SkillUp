using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets
{
    public class Assignment : Asset
    {
        public string Instruction { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
