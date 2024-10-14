using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class UserPurchasedCourseRepository : IUserPurchasedCourseRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<UserPurchasedCourse> _userPurchasedCourses;

        public UserPurchasedCourseRepository(CoursesDbContext context)
        {
            _context = context;
            _userPurchasedCourses = _context.UsersPurchasedCourses;
        }

        public async Task Add(UserPurchasedCourse userPurchasedCourse)
        {
            await _userPurchasedCourses.AddAsync(userPurchasedCourse);
            await _context.SaveChangesAsync();
        }
    }
}
