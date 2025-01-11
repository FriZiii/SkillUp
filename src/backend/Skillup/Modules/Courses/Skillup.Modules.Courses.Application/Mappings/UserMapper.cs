using Microsoft.Extensions.Logging;
using Riok.Mapperly.Abstractions;
using Skillup.Modules.Courses.Core.DTO.User;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Shared.Abstractions.Exceptions;
using Skillup.Shared.Abstractions.S3;

namespace Skillup.Modules.Courses.Application.Mappings
{
    [Mapper]
    public partial class UserMapper(IAmazonS3Service s3Service, ILogger _logger)
    {
        private readonly IAmazonS3Service _s3Service = s3Service;

        public UserDto UserToUserDto(User user, bool details)
        {
            string profilePicture;

            try
            {
                profilePicture = _s3Service.GetPulicUrl(S3FolderPaths.UserProfilePicture + user.ProfilePictureKey);
            }
            catch (Exception ex)
            {
                _logger.LogExceptions(ex);
                profilePicture = "https://default-url.com/default-profile-picture.jpg";
            }

            if (details)
            {
                return new DetailedUserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    ProfilePicture = new Uri(profilePicture),
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
                    ProfilePicture = new Uri(profilePicture),
                };
            }
        }
    }
}
