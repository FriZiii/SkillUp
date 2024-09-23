using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Repositories;

namespace Skillup.Modules.Auth.Core.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly AuthDbContext context;
        private readonly DbSet<User> _users;

        public UserRepository(AuthDbContext _context)
        {
            context = _context;
            _users = _context.Users;
        }

        public Task Add(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> Get(string email)
        {
            throw new NotImplementedException();
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
