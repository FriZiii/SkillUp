using MediatR;
using Skillup.Modules.Auth.Core.Features.Requests.User;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Auth;

namespace Skillup.Modules.Auth.Core.Features.Handlers.User
{
    internal class ChangeUserStateHandler : IRequestHandler<ChangeUserStateRequest>
    {
        private readonly IUserRepository _userRepository;

        public ChangeUserStateHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(ChangeUserStateRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId == request.RequestingUserId)
            {
                throw new InvalidOperationException("Users cannot change their own state.");
            }

            if (!await UserHasPermission(request.RequestingUserId))
            {
                throw new UnauthorizedAccessException("Only admin or moderator can change user state.");
            }

            await _userRepository.ChangeState(request.UserId, request.State);
            // TODO: Add logging here
        }

        private async Task<bool> UserHasPermission(Guid requestingUserId)
        {
            var userRole = await _userRepository.GetUserRole(requestingUserId);
            return userRole == UserRole.Admin || userRole == UserRole.Moderator;
        }
    }
}
