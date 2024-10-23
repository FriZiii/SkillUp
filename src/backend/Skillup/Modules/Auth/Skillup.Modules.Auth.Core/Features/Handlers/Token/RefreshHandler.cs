using MediatR;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Exceptions;
using Skillup.Modules.Auth.Core.Features.Commands.Token;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Modules.Auth.Core.Services;
using Skillup.Shared.Abstractions.Auth;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Token
{
    internal class RefreshHandler : IRequestHandler<RefreshRequest>
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

        public async Task Handle(RefreshRequest request, CancellationToken cancellationToken)
        {
            var userId = _authManager.GetUserIdFromExpiredToken(request.AccessToken) ?? throw new Exception();

            var user = await _userRepository.Get(userId) ?? throw new Exception();

            if (user.State == UserState.Locked)
            {
                throw new Exception();
            }

            try
            {
                var tokens = _authManager.RefreshAccessToken(request.RefreshToken, user.Id, user.Role);
                _authTokenStorage.SetToken(request.Id, tokens);
                //TODO : LOGS
            }
            catch (Exception ex)
            {
                //TODO : LOGS
                throw new TokenValidationFailed();
            }
        }
    }
}
