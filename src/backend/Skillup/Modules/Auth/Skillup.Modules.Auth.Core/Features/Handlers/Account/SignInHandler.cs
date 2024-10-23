using MediatR;
using Microsoft.AspNetCore.Identity;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Exceptions;
using Skillup.Modules.Auth.Core.Features.Commands.Account;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Modules.Auth.Core.Services;
using Skillup.Shared.Abstractions.Auth;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Account
{
    internal class SignInHandler(IUserRepository userRepository,
        IPasswordHasher<Entities.User> passwordHasher,
        IAuthManager authManager,
        IAuthTokenStorage authTokenStorage
        ) : IRequestHandler<SignInRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<Entities.User> _passwordHasher = passwordHasher;
        private readonly IAuthManager _authManager = authManager;
        private readonly IAuthTokenStorage _authTokenStorage = authTokenStorage;

        public async Task Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Email.ToLowerInvariant()) ?? throw new InvalidCredentialsException();

            if (user.State != UserState.Active)
            {
                throw new UserNotActiveException(user.Id);
            }

            if (_passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) ==
                PasswordVerificationResult.Failed)
            {
                throw new InvalidCredentialsException();
            }

            var tokens = _authManager.CreateTokens(user.Id, user.Role);
            _authTokenStorage.SetToken(request.Id, tokens);
            //TODO : LOGS
        }
    }
}
