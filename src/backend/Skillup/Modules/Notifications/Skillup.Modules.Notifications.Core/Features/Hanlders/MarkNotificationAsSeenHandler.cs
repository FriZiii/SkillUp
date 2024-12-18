using MediatR;
using Skillup.Modules.Notifications.Core.Features.Requests;
using Skillup.Modules.Notifications.Core.Repositories;

namespace Skillup.Modules.Notifications.Core.Features.Hanlders
{
    internal class MarkNotificationAsSeenHandler(INotificationRepository notificationRepository) : IRequestHandler<MarkNotificationAsSeenRequest>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;

        public async Task Handle(MarkNotificationAsSeenRequest request, CancellationToken cancellationToken)
        {
            await _notificationRepository.MarkAsSeen(request.NotificationId);
        }
    }
}
