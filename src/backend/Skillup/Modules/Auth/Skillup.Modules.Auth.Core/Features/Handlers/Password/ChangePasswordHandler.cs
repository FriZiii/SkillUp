using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Skillup.Modules.Auth.Core.Features.Requests.Password;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Events.Auth;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Password
{
    internal class ChangePasswordHandler(IUserRepository userRepository, IPasswordHasher<Entities.User> passwordHasher, IPublishEndpoint publishEndpoint) : IRequestHandler<ChangePasswordRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher<Entities.User> _passwordHasher = passwordHasher;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId) ?? throw new NotFoundException($"Item with ID {request.UserId} not found");

            if (_passwordHasher.VerifyHashedPassword(user, user.Password, request.CurrentPassword) ==
                PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid password");
            }

            user.Password = _passwordHasher.HashPassword(user, request.NewPassword); ;
            await _userRepository.Update(user);
            await _publishEndpoint.Publish(new PasswordChanged(user.Id));
        }
    }
}
