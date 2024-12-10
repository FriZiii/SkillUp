using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO.Review;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    internal partial class CourseReviewCommentMapper
    {
        public CourseReviewCommentDto CourseReviewCommentToDto(CourseReviewComment courseReviewComment)
        {
            return new CourseReviewCommentDto()
            {
                CourseElementId = courseReviewComment.CourseElementId,
                CreatedAt = courseReviewComment.CreatedAt,
                Id = courseReviewComment.Id,
                IsResolved = courseReviewComment.IsResolved,
            };
        }
    }
}
