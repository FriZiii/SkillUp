using MediatR;

namespace Skillup.Modules.Auth.Core.Commands.Handlers
{
    internal class ChangePasswordHandler : IRequestHandler<ChangePassword>
    {
        public Task Handle(ChangePassword request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
