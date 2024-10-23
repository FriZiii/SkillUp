using MediatR;
using Skillup.Modules.Auth.Core.Features.Commands.Account;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Account
{
    internal class SignOutHandler : IRequestHandler<SignOutRequest>
    {
        public async Task Handle(SignOutRequest request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //TODO : LOGS User with id {request.UserId} logged out 
        }
    }
}
