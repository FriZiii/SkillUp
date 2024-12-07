using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO.Review;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    internal partial class CourseReviewMapper
    {
        public CourseReviewDto CourseReviewToDto(CourseReview courseReview)
        {
            var commentsMapper = new CourseReviewCommentMapper();
            var comments = courseReview.Comments.Select(commentsMapper.CourseReviewCommentToDto);

            return new CourseReviewDto()
            {
                CourseId = courseReview.CourseId,
                Id = courseReview.Id,
                CreatedAt = courseReview.CreatedAt,
                FinalizedAt = courseReview.FinalizedAt,
                Status = courseReview.Status,
                Comments = comments
            };
        }
    }
}
