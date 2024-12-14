﻿using MediatR;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<RefreshHandler> _logger;

        public RefreshHandler(IAuthManager authManager, IUserRepository userRepository, IAuthTokenStorage authTokenStorage, ILogger<RefreshHandler> logger)
        {
            _authManager = authManager;
            _userRepository = userRepository;
            _authTokenStorage = authTokenStorage;
            _logger = logger;
        }

        public async Task Handle(RefreshRequest request, CancellationToken cancellationToken)
        {
            var userId = _authManager.GetUserIdFromExpiredToken(request.AccessToken) ?? throw new Exception();

            var user = await _userRepository.Get(userId) ?? throw new Exception();

            if (user.State == UserState.Locked)
            {
                _logger.LogError("User is in locked state");
                throw new Exception(); //TODO: Custom Ex: User is in locked
            }

            try
            {
                var tokens = _authManager.RefreshAccessToken(request.RefreshToken, user.Id, user.Role);
                _authTokenStorage.SetToken(request.Id, tokens);
                _logger.LogInformation("Token refreshed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new TokenValidationFailed();
            }
        }
    }
}
