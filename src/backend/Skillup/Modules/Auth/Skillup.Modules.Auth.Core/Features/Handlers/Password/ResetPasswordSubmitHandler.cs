﻿using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Skillup.Modules.Auth.Core.Features.Requests.Password;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Events.Auth;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Password
{
    internal class ResetPasswordSubmitHandler(
        IPasswordResetRepository passwordResetRepository,
        IClock clock,
        IUserRepository userRepository,
        IPasswordHasher<Entities.User> passwordHasher,
        IPublishEndpoint publishEndpoint)
        : IRequestHandler<ResetPasswordSubmitRequest>
    {
        private readonly IPasswordResetRepository _passwordResetRepository = passwordResetRepository;
        private readonly IClock _clock = clock;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<Entities.User> _passwordHasher = passwordHasher;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(ResetPasswordSubmitRequest request, CancellationToken cancellationToken)
        {
            var passwordReset = await _passwordResetRepository.GetByToken(request.Token) ?? throw new Exception(); // TODO: Custom ex

            if (!passwordReset.IsActive)
                throw new Exception();

            if (_clock.CurrentDate() > passwordReset.ExpiresAt)
                throw new Exception();

            var user = await _userRepository.Get(passwordReset.UserId) ?? throw new Exception();

            user.Password = _passwordHasher.HashPassword(user, request.NewPassword); ;
            await _userRepository.Update(user);

            passwordReset.IsActive = false;
            await _passwordResetRepository.Update(passwordReset);
            await _publishEndpoint.Publish(new PasswordChanged(user.Id));
        }
    }
}