using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.Element.Assets
{
    public class Assignment : Asset
    {
        public string Instruction { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
