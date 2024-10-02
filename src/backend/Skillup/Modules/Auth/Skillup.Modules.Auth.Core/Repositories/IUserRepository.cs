using Skillup.Modules.Auth.Core.Entities;

namespace Skillup.Modules.Auth.Core.Repositories
{
    internal interface IUserRepository
    {
        Task<User?> Get(Guid id);
        Task<User?> Get(string email);
        Task Add(User user);
        Task Update(User user);
        Task ChangeState(Guid userId, UserState state);
    }
}
