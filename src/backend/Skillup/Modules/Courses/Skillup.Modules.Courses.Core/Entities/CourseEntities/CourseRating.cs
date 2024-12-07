using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Core.Entities.CourseEntities
{
    public class CourseRating
    {
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid RatedById { get; set; }
        public User RatedBy { get; set; }

        public int Stars { get; set; }
        public string Feedback { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
