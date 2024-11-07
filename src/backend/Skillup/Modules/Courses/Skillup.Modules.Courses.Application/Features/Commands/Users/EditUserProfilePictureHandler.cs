using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Options;
using Skillup.Modules.Courses.Core.Requests.Commands.Users;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using Skillup.Shared.Abstractions.S3;
using System.Net;

namespace Skillup.Modules.Courses.Application.Features.Commands.Users
{
    internal class EditUserProfilePictureHandler : IRequestHandler<EditUserProfilePictureRequest>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAmazonS3Service _s3Service;
        private readonly ILogger<EditUserProfilePictureHandler> _logger;

        public EditUserProfilePictureHandler(IUserRepository userRepository, IAmazonS3Service s3Service, ILogger<EditUserProfilePictureHandler> logger)
        {
            _userRepository = userRepository;
            _s3Service = s3Service;
            _logger = logger;
        }

        public async Task Handle(EditUserProfilePictureRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetById(request.UserId) ?? throw new UserNotFoundException(request.UserId);

            var key = user.ProfilePictureKey;
            if (user.ProfilePictureKey == CourseModuleOptions.DefaultValues.DefaultUserProfilePictureKey)
            {
                key = Guid.NewGuid().ToString();
            }

            var response = await _s3Service.Upload(request.File, S3FolderPaths.UserProfilePicture + key);
            if (response?.HttpStatusCode == HttpStatusCode.OK)
            {
                user.ProfilePictureKey = key;
                await _userRepository.Edit(user);
                _logger.LogInformation("User profile picture edited");
            }
        }
    }
}
