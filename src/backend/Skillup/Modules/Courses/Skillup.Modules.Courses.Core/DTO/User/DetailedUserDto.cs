using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Core.DTO.User
{
    public record DetailedUserDto : UserDto
    {
        public UserDetails Details { get; set; }
        public SocialMediaLinks SocialMediaLinks { get; set; }
        public PrivacySettings PrivacySettings { get; set; }
    }
}
