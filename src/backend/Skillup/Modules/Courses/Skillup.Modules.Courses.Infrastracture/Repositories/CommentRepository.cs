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
            var rootComments = await _comments
                .Where(c => c.ElementId == elementId && c.ParentCommentId == null)
                .Include(c => c.Replies)
                .Include(c => c.Likes)
                .Include(c => c.Author)
                .ToListAsync();

            foreach (var comment in rootComments)
            {
                await PopulateRepliesRecursivelyAsync(comment);
            }

            ProcessDeletedComments(rootComments);

            return rootComments ?? Enumerable.Empty<Comment>();
        }

        private async Task PopulateRepliesRecursivelyAsync(Comment comment)
        {
            if (comment.Replies?.Any() != true) return;

            foreach (var reply in comment.Replies)
            {
                reply.Replies = await _comments
                    .Where(c => c.ParentCommentId == reply.Id)
                    .Include(c => c.Replies)
                    .Include(c => c.Likes)
                    .Include(c => c.Author)
                    .ToListAsync();

                await PopulateRepliesRecursivelyAsync(reply);
            }
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
