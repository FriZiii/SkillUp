using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Comments;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    internal partial class CommentsMapper
    {
        public static CommentDto CommentToDto(Comment comment, Guid userId, IAmazonS3Service amazonS3Service)
        {
            var userMapper = new UserMapper(amazonS3Service);

            return new CommentDto()
            {
                Id = comment.Id,
                ElementId = comment.ElementId,
                Author = userMapper.UserToUserDto(comment.Author, false),
                CreatedAt = comment.CreatedAt,
                Content = comment.Content,
                LikesCount = comment.Likes.Count,
                IsLiked = comment.Likes.Any(x => x.LikerId == userId),
                Replies = comment.Replies.Select(x => CommentToDto(x, userId, amazonS3Service)).ToList() ?? []
            };
        }
    }
}
