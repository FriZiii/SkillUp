using MediatR;
using Skillup.Shared.Abstractions.Events.Notifications;

namespace Skillup.Modules.Notifications.Core.Features.Requests
{
    internal record PublishNotificationRequest(NotifitationType Type, Guid UserId, string Message) : IRequest;
}
