using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Infrastracture.Seeders
{
    internal class AuthorSeeder
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<User> _users;

        public AuthorSeeder(CoursesDbContext context)
        {
            _context = context;
            _users = _context.Users;
        }
        public async Task Seed()
        {
            if (!await _users.AnyAsync())
            {
                await SeedAuthor();
            }
        }

        private async Task SeedAuthor()
        {
            var author = new User()
            {
                Id = new Guid("cece0863-6203-4a9e-b30a-57cbbac3c116"),
                Email = "author@skillup.com",
                FirstName = "John",
                LastName = "Smith",
                ProfilePicture = new Uri("https://cdn.pixabay.com/photo/2016/04/01/11/11/boy-1300242_1280.png"),
                Details = new UserDetails("Teacher", "I am a teacher"),
                SocialMediaLinks = new(),
                PrivacySettings = new PrivacySettings()
            };

            await _users.AddAsync(author);
            await _context.SaveChangesAsync();
        }
    }
}
