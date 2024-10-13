using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO.User;
using Skillup.Modules.Courses.Core.Entities.UserEntities;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class UserMapper
    {
        public UserDto UserToUserDto(User user, bool details)
        {
            if (details)
            {
                return new DetailedUserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ProfilePicture = user.ProfilePicture,
                    Details = user.Details,
                    SocialMediaLinks = user.SocialMediaLinks,
                    PrivacySettings = user.PrivacySettings,
                };
            }
            else
            {
                return new BasicUserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ProfilePicture = user.ProfilePicture,
                };
            }
        }
    }
}
