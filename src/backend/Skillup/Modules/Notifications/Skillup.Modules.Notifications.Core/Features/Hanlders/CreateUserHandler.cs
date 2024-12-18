using MediatR;
using Skillup.Modules.Notifications.Core.Features.Requests;

namespace Skillup.Modules.Notifications.Core.Features.Hanlders
{
    internal class CreateUserHandler : IRequestHandler<CreateUserRequest>
    {
        public Task Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
