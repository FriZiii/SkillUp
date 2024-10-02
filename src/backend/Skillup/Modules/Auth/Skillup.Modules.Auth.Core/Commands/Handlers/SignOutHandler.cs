using MediatR;

namespace Skillup.Modules.Auth.Core.Commands.Handlers
{
    internal class SignOutHandler : IRequestHandler<SignOut>
    {
        public async Task Handle(SignOut request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //TODO : LOGS
        }
    }
}
