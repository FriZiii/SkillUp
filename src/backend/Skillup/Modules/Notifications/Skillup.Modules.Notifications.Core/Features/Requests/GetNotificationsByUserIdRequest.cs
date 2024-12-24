using MediatR;
using Skillup.Modules.Notifications.Core.Entitites;

namespace Skillup.Modules.Notifications.Core.Features.Requests
{
    internal record GetNotificationsByUserIdRequest(Guid UserId) : IRequest<IEnumerable<Notification>>;
}
