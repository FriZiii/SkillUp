using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Exceptions;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Modules.Auth.Core.Services;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Auth.Core.Commands.Handlers
{
    internal class SignInHandler(IUserRepository userRepository,
        RegistrationOptions registrationOptions,
        IPublishEndpoint publishEndpoint,
        IPasswordHasher<User> passwordHasher,
        IAuthManager authManager,
        IAuthTokenStorage authTokenStorage,
        IClock clock) : IRequestHandler<SignIn>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly RegistrationOptions _registrationOptions = registrationOptions;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IAuthManager _authManager = authManager;
        private readonly IAuthTokenStorage _authTokenStorage = authTokenStorage;
        private readonly IClock _clock = clock;

        public async Task Handle(SignIn request, CancellationToken cancellationToken)
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

            //TODO : ROLES
            var tokens = _authManager.CreateTokens(user.Id);
            _authTokenStorage.SetToken(request.Id, tokens);
            //TODO : LOGS
        }
    }
}
