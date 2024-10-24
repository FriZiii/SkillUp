using MediatR;
using Skillup.Modules.Auth.Core.Features.Requests.User;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Auth;

namespace Skillup.Modules.Auth.Core.Features.Handlers.User
{
    internal class ChangeUserRoleHandler : IRequestHandler<ChangeUserRoleRequest>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserRoleHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(ChangeUserRoleRequest request, CancellationToken cancellationToken)
        {
            var requestingUserRole = await _userRepository.GetUserRole(request.RequestingUserId);
            var targetUserRole = await _userRepository.GetUserRole(request.UserId);


            if (request.UserId == request.RequestingUserId)
            {
                if (request.Role == UserRole.Instructor)
                {
                    await ChangeRoleAndLog(request.UserId, request.Role);
                    return;
                }
                throw new UnauthorizedAccessException("Users cannot change their own role to anything other than Instructor.");
            }
            else
            {
                ValidateRoleChangeAuthorization(requestingUserRole, request.Role, targetUserRole);
                await ChangeRoleAndLog(request.UserId, request.Role);
            }
        }

        private async Task ChangeRoleAndLog(Guid userId, UserRole newRole)
        {
            await _userRepository.ChangeRole(userId, newRole);
            // TODO: Add logging here
        }

        private void ValidateRoleChangeAuthorization(UserRole requestingUserRole, UserRole newRole, UserRole targetUserRole)
        {
            if (newRole == UserRole.Admin && requestingUserRole != UserRole.Admin)
            {
                throw new UnauthorizedAccessException("Only an Admin can assign the Admin role.");
            }

            if (requestingUserRole != UserRole.Admin && requestingUserRole != UserRole.Moderator)
            {
                throw new UnauthorizedAccessException("Only Admins and Moderators can change roles.");
            }

            if ((targetUserRole == UserRole.Admin && newRole != UserRole.Moderator) ||
                (targetUserRole == UserRole.Moderator && newRole == UserRole.Instructor))
            {
                throw new UnauthorizedAccessException("Users cannot decrease their own role.");
            }
        }
    }
}
