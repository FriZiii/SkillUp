using MediatR;

namespace Skillup.Modules.Auth.Core.Commands.Handlers
{
    internal class SignUpHandler : IRequestHandler<SignUp>
    {
        public Task Handle(SignUp request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
