using Skillup.Modules.Notifications.Core.Entitites;

namespace Skillup.Modules.Notifications.Core.Repositories
{
    internal interface IUserRepository
    {
        Task Add(User user);
    }
}
