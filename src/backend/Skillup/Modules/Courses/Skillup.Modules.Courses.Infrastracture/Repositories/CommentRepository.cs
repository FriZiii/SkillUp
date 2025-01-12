using Microsoft.EntityFrameworkCore;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Comments;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Courses.Infrastracture.Repositories
{
    internal class CommentRepository : ICommentRepository
    {
        private readonly CoursesDbContext _context;
        private readonly DbSet<Comment> _comments;
        private readonly DbSet<Like> _likes;
        private readonly DbSet<User> _users;

        public CommentRepository(CoursesDbContext context)
        {
            _context = context;
            _comments = _context.Comments;
            _likes = _context.Likes;
            _users = _context.Users;
        }

        public async Task Add(Comment comment)
        {
            await _comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid commentId)
        {
            var commentToDelete = await _comments.FirstOrDefaultAsync(c => c.Id == commentId) ?? throw new NotFoundException($"Comment with ID {commentId} not found");
            commentToDelete.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetByElementId(Guid elementId)
        {
            var comments = await _comments
                .Where(c => c.ElementId == elementId)
                .Include(c => c.Likes)
                .ToListAsync();

            var authorIds = comments.Select(x => x.AuthorId).Distinct();
            var authors = await _users
                .Where(u => authorIds.Contains(u.Id))
                .ToListAsync();

            var authorsDict = authors.ToDictionary(a => a.Id, a => a);
            foreach (var comment in comments)
            {
                comment.Author = authorsDict[comment.AuthorId];
            }

            var commentLookup = comments.ToLookup(c => c.ParentCommentId);
            void AssignReplies(ICollection<Comment> parentComments)
            {
                foreach (var comment in parentComments)
                {
                    comment.Replies = commentLookup[comment.Id].ToList();

                    AssignReplies(comment.Replies);
                }
            }

            var rootComments = comments.Where(c => c.ParentCommentId == null).ToList();
            AssignReplies(rootComments);

            ProcessDeletedComments(rootComments);

            return rootComments ?? Enumerable.Empty<Comment>();
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
