using MediatR;
using Skillup.Modules.Courses.Core.Entities.UserEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests;

namespace Skillup.Modules.Courses.Application.Features.Commands
{
    internal class EditUserPrivacySettingsHandler : IRequestHandler<EditUserPrivacySettingsRequest>
    {
        private readonly IUserRepository _userRepository;

        public EditUserPrivacySettingsHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(EditUserPrivacySettingsRequest request, CancellationToken cancellationToken)
        {
            var privacySettings = new PrivacySettings()
            {
                ShowCoursesOnUserProfile = request.ShowCoursesOnUserProfile,
                IsAccountPublicForLoggedInUsers = request.IsAccountPublicForLoggedInUsers,
            };

            await _userRepository.EditUserPrivacySettings(request.UserId, privacySettings);
        }
    }
}
