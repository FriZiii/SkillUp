using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests.Commands.Users;

namespace Skillup.Modules.Courses.Application.Features.Commands.Users
{
    internal class EditUserPrivacySettingsHandler : IRequestHandler<EditUserPrivacySettingsRequest>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<EditUserPrivacySettingsHandler> _logger;

        public EditUserPrivacySettingsHandler(IUserRepository userRepository, ILogger<EditUserPrivacySettingsHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(EditUserPrivacySettingsRequest request, CancellationToken cancellationToken)
        {
            var privacySettings = new PrivacySettings()
            {
                ShowCoursesOnUserProfile = request.ShowCoursesOnUserProfile,
                IsAccountPublicForLoggedInUsers = request.IsAccountPublicForLoggedInUsers,
            };

            await _userRepository.EditUserPrivacySettings(request.UserId, privacySettings);
            _logger.LogInformation("User privacy setting edited");
        }
    }
}
