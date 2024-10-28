using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Core.Interfaces
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task Edit(User user);
        Task<User?> GetById(Guid userId);
        Task EditUserPrivacySettings(Guid userId, PrivacySettings privacySettings);
    }
}
