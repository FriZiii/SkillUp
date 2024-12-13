using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class CourseUserProgressRepository : ICourseUserProgressRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<CourseUserProgess> _coursUserProgress;

        public CourseUserProgressRepository(CoursesDbContext context)
        {
            _context = context;
            _coursUserProgress = _context.CourseUserProgess;
        }
        public async Task Add(CourseUserProgess userProgess)
        {
            await _coursUserProgress.AddAsync(userProgess);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var toDelete = await _coursUserProgress.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception(); // TODO: Custom ex: _coursUserProgress with id doesnt exist
            _coursUserProgress.Remove(toDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseUserProgess?> GetByUserAndElementId(Guid userId, Guid elementId)
            => await _coursUserProgress.FirstOrDefaultAsync(x => x.UserId == userId && x.ElementId == elementId);

        public async Task<IEnumerable<CourseUserProgess>> GetByCourseAndUserId(Guid courseId, Guid userId)
            => await _coursUserProgress.Where(x => x.UserId == userId && x.CourseId == courseId).ToListAsync();

        public async Task<IEnumerable<CourseUserProgess>> GetByUserId(Guid userId)
            => await _coursUserProgress.Where(x => x.UserId == userId).ToListAsync();
    }
}
