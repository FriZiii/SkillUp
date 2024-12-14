using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent;
using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class CourseUserProgess
    {
        public Guid Id { get; set; }

        public Guid ElementId { get; set; }
        public Element Element { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
