using MediatR;

namespace Skillup.Modules.Notifications.Core.Features.Requests
{
    internal record CreateUserRequest(Guid UserId) : IRequest;
}
