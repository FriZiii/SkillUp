using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Features.Commands.Account;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Abstractions.Events.Auth;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Account
{
    internal class SignUpHandler(IUserRepository userRepository,
        RegistrationOptions registrationOptions,
        IPublishEndpoint publishEndpoint,
        IPasswordHasher<Entities.User> passwordHasher,
        IClock clock,
        ILogger<SignUpHandler> logger) : IRequestHandler<SignUpRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly RegistrationOptions _registrationOptions = registrationOptions;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
        private readonly IPasswordHasher<Entities.User> _passwordHasher = passwordHasher;
        private readonly IClock _clock = clock;
        private readonly ILogger<SignUpHandler> _logger = logger;

        public async Task Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            if (!_registrationOptions.Enabled)
            {
                throw new UnauthorizedException("Signup disabled");
            }

            var email = request.Email.ToLowerInvariant();
            var provider = email.Split("@").Last();
            if (_registrationOptions.InvalidEmailProviders?.Any(provider.Contains) is true)
            {
                throw new UnauthorizedException("Invalid email adress");
            }

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length is > 100 or < 6)
            {
                throw new UnauthorizedException("Password not matching the criteria");
            }

            var user = await _userRepository.Get(email);
            if (user is not null)
            {
                throw new UnauthorizedException("Email adress is already in use");
            }

            var now = _clock.CurrentDate();

            user = new Entities.User(request.UserId, email, UserRole.User, UserState.Inactive, now, Guid.NewGuid(), now.AddHours(24));
            var password = _passwordHasher.HashPassword(user, request.Password);
            user.Password = password;

            await _userRepository.Add(user);

            await _publishEndpoint.Publish(new SignedUp(user.Id, user.Email, request.AllowMarketingEmails, user.ActivationToken, user.TokenExpiration), cancellationToken);
            _logger.LogInformation("User signed up");
        }
    }
}