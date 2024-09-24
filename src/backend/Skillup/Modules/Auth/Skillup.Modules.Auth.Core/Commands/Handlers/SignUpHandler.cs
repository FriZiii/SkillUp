using MediatR;
using Microsoft.AspNetCore.Identity;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Exceptions;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Auth.Core.Commands.Handlers
{
    internal class SignUpHandler : IRequestHandler<SignUp>
    {
        private readonly IUserRepository _userRepository;
        private readonly RegistrationOptions _registrationOptions;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IClock _clock;

        public SignUpHandler(IUserRepository userRepository, RegistrationOptions registrationOptions, IPasswordHasher<User> passwordHasher, IClock clock)
        {
            _userRepository = userRepository;
            _registrationOptions = registrationOptions;
            _passwordHasher = passwordHasher;
            _clock = clock;
        }

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

            user = new User
            {
                Id = request.UserId,
                Email = email,
                CreatedAt = now,
                State = UserState.Inactive,
            };
            var password = _passwordHasher.HashPassword(user, request.Password);
            user.Password = password;

            await _userRepository.Add(user);

            //TODO : MESSAGE BROKER
            //TODO : LOGS
        }
    }
}
