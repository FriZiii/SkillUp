using Skillup.Modules.Finances.Core.Entities;

namespace Skillup.Modules.Finances.Core.Repositories
{
    internal interface IUserRepository
    {
        Task Add(User user);
        Task<User?> Get(Guid ownerId);
    }
}
