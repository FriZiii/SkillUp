using Skillup.Modules.Auth.Core.Entities;
using Skillup.Shared.Abstractions.Auth;

namespace Skillup.Modules.Auth.Core.Repositories
{
    internal interface IUserRepository
    {
        Task<User?> Get(Guid id);
        Task<User?> Get(string email);
        Task Add(User user);
        Task Update(User user);
        Task ChangeState(Guid userId, UserState state);
        Task ChangeRole(Guid userId, UserRole role);
        Task<UserRole> GetUserRole(Guid userId);
    }
}
