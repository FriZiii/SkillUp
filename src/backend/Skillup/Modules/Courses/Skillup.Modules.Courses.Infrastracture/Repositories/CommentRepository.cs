using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Comments;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class CommentRepository : ICommentRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Comment> _comments;
        private readonly DbSet<Like> _likes;

        public CommentRepository(CoursesDbContext context)
        {
            _context = context;
            _comments = _context.Comments;
            _likes = _context.Likes;
        }

        public async Task Add(Comment comment)
        {
            await _comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid commentId)
        {
            var commentToDelete = await _comments.FirstOrDefaultAsync(c => c.Id == commentId) ?? throw new Exception(); // TODO: Custom ex: Comments doesnt exist
            commentToDelete.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetByElementId(Guid elementId)
        {
            var commentsForElement = await _comments
                .Where(x => x.ElementId == elementId && x.ParentCommentId == null)
                .Include(x => x.Replies)
                    .ThenInclude(r => r.Replies)
                .Include(x => x.Likes)
                .Include(x => x.Author)
                .ToListAsync();

            ProcessDeletedComments(commentsForElement);

            return commentsForElement ?? Enumerable.Empty<Comment>();
        }

        private static void ProcessDeletedComments(IEnumerable<Comment> comments)
        {
            foreach (var comment in comments)
            {
                if (comment.IsDeleted)
                {
                    comment.Content = "[Comment deleted]";
                }

                if (comment.Replies != null && comment.Replies.Any())
                {
                    ProcessDeletedComments(comment.Replies);
                }
            }
        }

        public async Task ToggleLikeComment(Guid commentId, Guid userId)
        {
            var like = await _likes.FirstOrDefaultAsync(x => x.CommentId == commentId && x.LikerId == userId);
            if (like is null)
            {
                like = new Like()
                {
                    CommentId = commentId,
                    LikerId = userId
                };
                await _likes.AddAsync(like);
                await _context.SaveChangesAsync();
            }
            else
            {
                _likes.Remove(like);
                await _context.SaveChangesAsync();
            }
        }
    }
}
