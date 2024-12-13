using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class CourseReviewRepository(CoursesDbContext context) : ICourseReviewRepository
    {
        private readonly CoursesDbContext _context = context;
        private readonly DbSet<CourseReview> _courseReviews = context.CourseReviews;

        public async Task Add(CourseReview courseReview)
        {
            await _courseReviews.AddAsync(courseReview);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseReview?> Get(Guid reviewId)
            => await _courseReviews
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == reviewId);

        public async Task<IEnumerable<CourseReview>> GetWithStatus(ReviewStatus status)
            => await _courseReviews.Where(x => x.Status == status).Include(x => x.Comments)
                .ToListAsync();

        public async Task<IEnumerable<CourseReview>> GetByCourse(Guid courseId)
            => await _courseReviews.Where(x => x.CourseId == courseId).Include(x => x.Comments)
                .ToListAsync();

        public async Task<CourseReview?> GetLatestByCourse(Guid courseId)
        => await _courseReviews
            .Where(x => x.CourseId == courseId)
            .OrderByDescending(x => x.CreatedAt)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync();

        public async Task Update(CourseReview review)
        {
            var reviewToEdit = await _courseReviews.FirstOrDefaultAsync(x => x.Id == review.Id) ?? throw new Exception(); // TODO: Custom ex: review with id doesnt exist
            reviewToEdit.FinalizedAt = review.FinalizedAt;
            review.Status = review.Status;
            await _context.SaveChangesAsync();
        }
    }
}
