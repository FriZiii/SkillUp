using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(CoursesDbContext context)
        {
            _context = context;
            _users = _context.Users;
        }

        public async Task Add(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(User user)
        {
            var userToEdit = await _users.FirstOrDefaultAsync(x => x.Id == user.Id) ?? throw new Exception(); //TODO: custom exception

            userToEdit.Email = user.Email;
            userToEdit.FirstName = user.FirstName;
            userToEdit.LastName = user.LastName;
            userToEdit.Details = user.Details;
            userToEdit.SocialMediaLinks = user.SocialMediaLinks;

            await _context.SaveChangesAsync();
        }

        public async Task EditUserPrivacySettings(Guid userId, PrivacySettings privacySettings)
        {
            var userToEdit = await _users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new Exception(); //TODO: custom exception

            userToEdit.PrivacySettings = privacySettings;

            await _context.SaveChangesAsync();
        }
    }
}
