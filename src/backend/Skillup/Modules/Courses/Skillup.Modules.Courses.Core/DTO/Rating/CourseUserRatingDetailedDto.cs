using Skillup.Modules.Courses.Core.DTO.User;

namespace Skillup.Modules.Courses.Core.DTO.Rating
{
    public class CourseUserRatingDetailedDto : CourseUserRatingDto
    {
        public BasicUserDto RatedBy { get; set; }
    }
}
