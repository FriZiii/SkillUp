using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Features.Commands.Account;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Modules.Auth.Core.Services;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Account
{
    internal class SignInHandler(IUserRepository userRepository,
        IPasswordHasher<Entities.User> passwordHasher,
        IAuthManager authManager,
        IAuthTokenStorage authTokenStorage,
        ILogger<SignInHandler> logger
        ) : IRequestHandler<SignInRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<Entities.User> _passwordHasher = passwordHasher;
        private readonly IAuthManager _authManager = authManager;
        private readonly IAuthTokenStorage _authTokenStorage = authTokenStorage;
        private readonly ILogger<SignInHandler> _logger = logger;

        public async Task Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Email.ToLowerInvariant()) ?? throw new UnauthorizedException("Invalid credencials");

            if (user.State != UserState.Active)
            {
                throw new UnauthorizedException("This account has not been activated. Check your email to activate your account");
            }

            if (_passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) ==
                PasswordVerificationResult.Failed)
            {
                throw new UnauthorizedException("Invalid credencials");
            }

            var tokens = _authManager.CreateTokens(user.Id, user.Role);
            _authTokenStorage.SetToken(request.Id, tokens);
            _logger.LogInformation("User signed in");
        }
    }
}
