using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<User> _users;

        public UserRepository(CoursesDbContext context)
        {
            _context = context;
            _users = context.Users;
        }

        public async Task<User?> GetById(Guid userId)
            => await _users.FirstOrDefaultAsync(_ => _.Id == userId);

        public async Task Add(User user)
        {
            await _users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(User user)
        {
            var userToEdit = await _users.FirstOrDefaultAsync(x => x.Id == user.Id) ?? throw new UserNotFoundException(user.Id);

            userToEdit.Email = user.Email;
            userToEdit.FirstName = user.FirstName;
            userToEdit.LastName = user.LastName;
            userToEdit.Details = user.Details;
            userToEdit.ProfilePictureKey = user.ProfilePictureKey;
            userToEdit.SocialMediaLinks = user.SocialMediaLinks;
            userToEdit.PrivacySettings = user.PrivacySettings;

            await _context.SaveChangesAsync();
        }

        public async Task EditUserPrivacySettings(Guid userId, PrivacySettings privacySettings)
        {
            var userToEdit = await _users.FirstOrDefaultAsync(x => x.Id == userId) ?? throw new UserNotFoundException(userId);

            userToEdit.PrivacySettings = privacySettings;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _users.ToListAsync();
        }
    }
}
