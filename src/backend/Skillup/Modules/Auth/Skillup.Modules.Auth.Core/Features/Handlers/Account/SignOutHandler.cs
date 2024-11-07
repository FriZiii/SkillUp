using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Auth.Core.Features.Commands.Account;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Account
{
    internal class SignOutHandler(ILogger<SignOutHandler> logger) : IRequestHandler<SignOutRequest>
    {
        private readonly ILogger<SignOutHandler> _logger = logger;

        public async Task Handle(SignOutRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            _logger.LogInformation($"User with id {request.UserId} logged out");
        }
    }
}
