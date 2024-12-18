using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Notifications.Core.Entitites;
using Skillup.Modules.Notifications.Core.Repositories;

namespace Skillup.Modules.Notifications.Core.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly NotificationsDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(NotificationsDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public async Task Add(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
