using MediatR;
using Skillup.Modules.Notifications.Core.Entitites;
using Skillup.Modules.Notifications.Core.Features.Requests;
using Skillup.Modules.Notifications.Core.Repositories;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Notifications.Core.Features.Hanlders
{
    internal class PublishNotificationHandler(INotificationRepository notificationRepository, IClock clock) : IRequestHandler<PublishNotificationRequest>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;
        private readonly IClock _clock = clock;

        public async Task Handle(PublishNotificationRequest request, CancellationToken cancellationToken)
        {
            await _notificationRepository.Add(new Notification()
            {
                Type = request.Type,
                Message = request.Message,
                Seen = false,
                CreatedAt = _clock.CurrentDate(),
                UserId = request.UserId,
            });
        }
    }
}
