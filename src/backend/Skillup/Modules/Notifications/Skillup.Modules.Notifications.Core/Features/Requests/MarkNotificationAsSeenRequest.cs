using MediatR;

namespace Skillup.Modules.Notifications.Core.Features.Requests
{
    internal record MarkNotificationAsSeenRequest(Guid NotificationId) : IRequest;
}
