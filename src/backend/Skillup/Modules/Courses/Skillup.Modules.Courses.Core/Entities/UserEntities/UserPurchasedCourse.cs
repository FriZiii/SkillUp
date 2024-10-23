namespace Skillup.Modules.Courses.Core.Entities.UserEntities
{
    public class UserPurchasedCourse
    {
        public UserPurchasedCourse(Guid userId, Guid courseId)
        {
            UserId = userId;
            CourseId = courseId;
        }

        public Guid UserId { get; private set; }
        public Guid CourseId { get; private set; }
    }
}
