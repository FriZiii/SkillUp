using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Auth.Core.DAL;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Seeder;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Auth.Core.Seeders
{
    internal class AccountSeeder : ISeeder
    {
        private readonly AuthDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClock _clock;
        private readonly DbSet<User> _users;

        public AccountSeeder(AuthDbContext context, IPasswordHasher<User> passwordHasher, IClock clock)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _clock = clock;
            _users = context.Users;
        }

        public async Task Seed()
        {
            if (!await _users.AnyAsync())
            {
                var users = new List<User>()
                {
                   CreateUser("inactive@skillup.com", "Skillup123!", UserRole.User, UserState.Inactive),
                   CreateUser("user@skillup.com", "Skillup123!", UserRole.User,UserState.Active),
                   CreateUser("admin@skillup.com", "Skillup123!", UserRole.Admin,UserState.Active),
                   CreateUser("moderator@skillup.com", "Skillup123!", UserRole.Moderator,UserState.Active),
                   CreateUser("author@skillup.com", "Skillup123!", UserRole.CourseAuthor,UserState.Active),
                };

                await _users.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }
        }

        private User CreateUser(string email, string password, UserRole role, UserState state)
        {
            var now = _clock.CurrentDate();

            var user = new User(Guid.NewGuid(), email, role, state, now, Guid.NewGuid(), now.AddHours(24));
            var hashedPassword = _passwordHasher.HashPassword(user, password);
            user.Password = hashedPassword;

            return user;
        }
    }
}
