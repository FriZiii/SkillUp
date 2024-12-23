using MassTransit;
using MediatR;
using Skillup.Modules.Auth.Core.Entities;
using Skillup.Modules.Auth.Core.Features.Requests.Password;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Events.Auth;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Password
{
    internal class ResetPasswordHandler(IUserRepository userRepository, IPasswordResetRepository passwordResetRepository, IPublishEndpoint publishEndpoint) : IRequestHandler<ResetPasswordRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordResetRepository _passwordResetRepository = passwordResetRepository;
        private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;

        public async Task Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Email);

            if (user is null)
                return;

            var passwordReset = new PasswordReset(user);
            await _passwordResetRepository.Add(passwordReset);

            await _publishEndpoint.Publish(new PasswordResetRequested(user.Id, passwordReset.Token));
        }
    }
}
