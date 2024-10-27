using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Mails.Core.DAL;
using Skillup.Modules.Mails.Core.Entities;
using Skillup.Modules.Mails.Core.Seeder.Data;
using Skillup.Shared.Abstractions.Seeder;
using System.Text.Json;

namespace Skillup.Modules.Mails.Core.Seeder
{
    internal class MailSeeder : ISeeder
    {
        private readonly MailDbContext _context;
        private readonly DbSet<User> _users;

        public MailSeeder(MailDbContext context)
        {
            _context = context;
            _users = context.Users;
        }
        public async Task Seed()
        {
            if (!await _users.AnyAsync())
            {
                await _users.AddRangeAsync(CreateUsers());
                await _context.SaveChangesAsync();
            }
        }
        private IEnumerable<User> CreateUsers()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Seeders", "Data");

            var jsonString = File.ReadAllText(Path.Combine(path, "mailUser-seeder-data.json"));

            JsonSerializerOptions options = new()
            {
                PropertyNameCaseInsensitive = true
            };

            var courseData = JsonSerializer.Deserialize<List<UserJsonModel>>(jsonString, options);

            return courseData!.Select(CreateUserFromJson);
        }

        private User CreateUserFromJson(UserJsonModel jsonModel)
        {
            var user = new User()
            {
                Id = jsonModel.Id,
                Email = jsonModel.Email,
                AllowMarketingEmails = jsonModel.AllowMarketingEmails,
            };

            return user;
        }
    }
}
