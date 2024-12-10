using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class ReviewCommentRepository : IReviewCommentRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<CourseReviewComment> _reviewComments;

        public ReviewCommentRepository(CoursesDbContext context)
        {
            _context = context;
            _reviewComments = context.ReviewComments;
        }
        public async Task Add(CourseReviewComment comment)
        {
            await _reviewComments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid commentId)
        {
            var commentToDelete = await _reviewComments.FirstOrDefaultAsync(x => x.Id == commentId) ?? throw new Exception(); //TODO: Custom ex
            _reviewComments.Remove(commentToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<CourseReviewComment?> Get(Guid commentId)
            => await _reviewComments.FirstOrDefaultAsync(x => x.Id == commentId);

        public async Task Update(CourseReviewComment comment)
        {
            var commentToEdit = await _reviewComments.FirstOrDefaultAsync(x => x.Id == comment.Id) ?? throw new Exception(); // TODO: Custom ex
            comment.IsResolved = comment.IsResolved;
            await _context.SaveChangesAsync();
        }
    }
}
