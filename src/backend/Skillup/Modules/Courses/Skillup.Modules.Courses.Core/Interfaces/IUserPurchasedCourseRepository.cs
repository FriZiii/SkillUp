using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IUserPurchasedCourseRepository
    {
        Task Add(UserPurchasedCourse userPurchasedCourse);
    }
}
