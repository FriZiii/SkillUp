using MediatR;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Features.Commands.Token;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Modules.Auth.Core.Services;
using Skillup.Shared.Abstractions.Auth;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Token
{
    internal class RefreshHandler : IRequestHandler<Refresh>
    {
        private readonly IAuthManager _authManager;
        private readonly IUserRepository _userRepository;
        private readonly IAuthTokenStorage _authTokenStorage;

        public RefreshHandler(IAuthManager authManager, IUserRepository userRepository, IAuthTokenStorage authTokenStorage)
        {
            _authManager = authManager;
            _userRepository = userRepository;
            _authTokenStorage = authTokenStorage;
        }

        public async Task Handle(Refresh request, CancellationToken cancellationToken)
        {
            var userId = _authManager.GetUserIdFromExpiredToken(request.AccessToken) ?? throw new Exception();

            var user = await _userRepository.Get(userId) ?? throw new Exception();

            if (user.State == UserState.Locked)
            {
                throw new Exception();
            }

            //TODO : ROLES
            var tokens = _authManager.RefreshAccessToken(request.RefreshToken, userId);
            _authTokenStorage.SetToken(request.Id, tokens);
            //TODO : LOGS
        }
    }
}
