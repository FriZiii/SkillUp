using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Auth.Core.DAL.Repositories
{
    internal class PasswordResetRepository : IPasswordResetRepository
    {
        private readonly AuthDbContext _context;
        private readonly DbSet<PasswordReset> _passwordResets;

        public PasswordResetRepository(AuthDbContext context)
        {
            _context = context;
            _passwordResets = _context.PasswordResets;
        }

        public async Task Add(PasswordReset passwordReset)
        {
            await _passwordResets.AddAsync(passwordReset);
            await _context.SaveChangesAsync();
        }

        public async Task Update(PasswordReset passwordReset)
        {
            var passwordResetToEdit = await _passwordResets.FirstOrDefaultAsync(x => x.Id == passwordReset.Id) ?? throw new NotFoundException($"Password reset token with ID {passwordReset.Id} doesnt exist");
            passwordResetToEdit.IsActive = passwordReset.IsActive;
            await _context.SaveChangesAsync();
        }

        public async Task<PasswordReset?> GetByToken(string token)
            => await _passwordResets.FirstOrDefaultAsync(r => r.Token == token);
    }
}
