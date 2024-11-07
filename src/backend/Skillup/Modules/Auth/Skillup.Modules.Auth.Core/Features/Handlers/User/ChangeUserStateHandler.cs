using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Auth.Core.Features.Requests.User;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Auth;

namespace Skillup.Modules.Auth.Core.Features.Handlers.User
{
    internal class ChangeUserStateHandler : IRequestHandler<ChangeUserStateRequest>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ChangeUserStateHandler> _logger;

        public ChangeUserStateHandler(IUserRepository userRepository, ILogger<ChangeUserStateHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
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
            _logger.LogInformation("User state changed");
        }

        private async Task<bool> UserHasPermission(Guid requestingUserId)
        {
            var userRole = await _userRepository.GetUserRole(requestingUserId);
            return userRole == UserRole.Admin || userRole == UserRole.Moderator;
        }
    }
}
