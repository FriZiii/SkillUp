using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Exceptions;
using Skillup.Modules.Auth.Core.Features.Commands.Account;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Events.Auth;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Account
{
    internal class SignUpHandler(IUserRepository userRepository,
        RegistrationOptions registrationOptions,
        IPublishEndpoint publishEndpoint,
        IPasswordHasher<User> passwordHasher,
        IClock clock) : IRequestHandler<SignUp>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly RegistrationOptions _registrationOptions = registrationOptions;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly IClock _clock = clock;

        public async Task Handle(SignUp request, CancellationToken cancellationToken)
        {
            if (!_registrationOptions.Enabled)
            {
                throw new SignUpDisabledException();
            }

            var email = request.Email.ToLowerInvariant();
            var provider = email.Split("@").Last();
            if (_registrationOptions.InvalidEmailProviders?.Any(provider.Contains) is true)
            {
                throw new InvalidEmailException(email);
            }

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length is > 100 or < 6)
            {
                throw new InvalidPasswordException("not matching the criteria");
            }

            var user = await _userRepository.Get(email);
            if (user is not null)
            {
                throw new EmailInUseException();
            }

            // TODO: ROLES

            var now = _clock.CurrentDate();

            user = new User(request.UserId, email, UserState.Inactive, now, Guid.NewGuid(), now.AddHours(24));
            var password = _passwordHasher.HashPassword(user, request.Password);
            user.Password = password;

            await _userRepository.Add(user);

            await _publishEndpoint.Publish(new SignedUp(user.Id, user.Email, user.ActivationToken, user.TokenExpiration), cancellationToken);
            //TODO : LOGS
        }
    }
}