using MediatR;
using Skillup.Modules.Notifications.Core.Entitites;
using Skillup.Modules.Notifications.Core.Features.Requests;
using Skillup.Modules.Notifications.Core.Repositories;

namespace Skillup.Modules.Notifications.Core.Features.Hanlders
{
    internal class GetNotificationsByUserIdHandler(INotificationRepository notificationRepository) : IRequestHandler<GetNotificationsByUserIdRequest, IEnumerable<Notification>>
    {
        private readonly INotificationRepository _notificationRepository = notificationRepository;

        public async Task<IEnumerable<Notification>> Handle(GetNotificationsByUserIdRequest request, CancellationToken cancellationToken)
        {
            return await _notificationRepository.GetByUserId(request.UserId);
        }
    }
}
