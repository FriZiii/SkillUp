using MediatR;

namespace Skillup.Modules.Auth.Core.Commands.Handlers
{
    internal class SignInHandler : IRequestHandler<SignIn>
    {
        public Task Handle(SignIn request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
