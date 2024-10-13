namespace Skillup.Modules.Courses.Core.Entities.UserEntities
{
    public class UserPurchasedCourse
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }
}
