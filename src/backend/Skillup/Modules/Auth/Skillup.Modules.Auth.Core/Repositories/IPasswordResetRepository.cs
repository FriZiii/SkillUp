using Skillup.Modules.Auth.Core.Entities;

namespace Skillup.Modules.Auth.Core.Repositories
{
    internal interface IPasswordResetRepository
    {
        Task Add(PasswordReset passwordReset);
        Task Update(PasswordReset passwordReset);
        Task<PasswordReset?> GetByToken(string token);
    }
}
