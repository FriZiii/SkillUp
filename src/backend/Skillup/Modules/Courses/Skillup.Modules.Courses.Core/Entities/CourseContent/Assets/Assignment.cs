using Skillup.Modules.Courses.Core.Entities.CourseContent.Assets.Exercises;

namespace Skillup.Modules.Courses.Core.Entities.CourseContent.Assets
{
    public class Assignment : Asset
    {
        public string Instruction { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
