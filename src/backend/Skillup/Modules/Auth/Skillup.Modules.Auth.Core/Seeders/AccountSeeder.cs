using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Auth.Core.DAL;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Seeders.Data;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Seeder;
using Skillup.Shared.Abstractions.Time;
using System.Text.Json;

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
                var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

                var jsonString = File.ReadAllText(Path.Combine(path, "user-seeder-data.json"));
                JsonSerializerOptions options = new()
                {
                    PropertyNameCaseInsensitive = true
                };

                var data = JsonSerializer.Deserialize<List<UserJsonModel>>(jsonString, options);
                var users = new List<User>()
                {
                   CreateUser(data[0].Id, data[0].Email, "Skillup123!", UserRole.User, UserState.Inactive),
                   CreateUser(data[1].Id, data[1].Email, "Skillup123!", UserRole.User,UserState.Active),
                   CreateUser(data[2].Id, data[2].Email, "Skillup123!", UserRole.Admin,UserState.Active),
                   CreateUser(data[3].Id, data[3].Email, "Skillup123!", UserRole.Moderator,UserState.Active),
                   CreateUser(data[4].Id, data[4].Email, "Skillup123!", UserRole.Instructor,UserState.Active),
                   CreateUser(data[5].Id, data[5].Email, "Skillup123!", UserRole.Instructor,UserState.Active),
                   CreateUser(data[6].Id, data[6].Email, "Skillup123!", UserRole.Instructor,UserState.Active),
                   CreateUser(data[7].Id, data[7].Email, "Skillup123!", UserRole.Instructor,UserState.Active),
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

        private User CreateUser(Guid id, string email, string password, UserRole role, UserState state)
        {
            var now = _clock.CurrentDate();

            var user = new User(id, email, role, state, now, Guid.NewGuid(), now.AddHours(24));
            var hashedPassword = _passwordHasher.HashPassword(user, password);
            user.Password = hashedPassword;

            return user;
        }
    }
}
