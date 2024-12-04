using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly FinancesDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(FinancesDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public async Task Add(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> Get(Guid ownerId)
            => await _users.FirstOrDefaultAsync(x => x.Id == ownerId);
    }
}
