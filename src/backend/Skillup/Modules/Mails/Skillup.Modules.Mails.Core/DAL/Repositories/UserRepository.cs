using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Mails.Core.Entities;
using Skillup.Modules.Mails.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skillup.Modules.Mails.Core.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly MailDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(MailDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public async Task Add(User user)
        {
            _users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> Get(Guid id)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public async Task Update(User user)
        {
            _users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
