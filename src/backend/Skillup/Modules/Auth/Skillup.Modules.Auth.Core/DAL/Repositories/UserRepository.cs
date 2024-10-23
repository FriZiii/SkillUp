using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Auth.Core.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(AuthDbContext context)
        {
            _context = context;
            _users = _context.Users;
        }

        public async Task Add(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> Get(Guid id)
            => await _users.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<User?> Get(string email)
         => await _users.SingleOrDefaultAsync(x => x.Email == email);

        public async Task Update(User user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeState(Guid userId, UserState state)
        {
            var userToChange = await _users.FirstOrDefaultAsync(x => x.Id.Equals(userId)) ?? throw new UserNotFoundException(userId);
            userToChange.State = state;
            await _context.SaveChangesAsync();
        }

        public async Task ChangeRole(Guid userId, UserRole role)
        {
            var userToChange = await _users.FirstOrDefaultAsync(x => x.Id.Equals(userId)) ?? throw new UserNotFoundException(userId);
            userToChange.Role = role;
            await _context.SaveChangesAsync();
        }

        public async Task<UserRole> GetUserRole(Guid userId)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Id.Equals(userId)) ?? throw new UserNotFoundException(userId);
            return user.Role;
        }
    }
}
