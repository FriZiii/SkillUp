using MediatR;

namespace Skillup.Modules.Auth.Core.Commands.Handlers
{
    internal class SignOutHandler : IRequestHandler<SignOut>
    {
        public Task Handle(SignOut request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
