using MassTransit;
using MediatR;
using Skillup.Modules.Notifications.Core.Features.Requests;
using Skillup.Shared.Abstractions.Events.Notifications;

namespace Skillup.Modules.Notifications.Core.Consumers
{
    internal class NotificationPublishedConsumer(IMediator mediator) : IConsumer<NotificationPublished>
    {
        private readonly IMediator _mediator = mediator;

        public async Task Consume(ConsumeContext<NotificationPublished> context)
        {
            await _mediator.Send(new PublishNotificationRequest(context.Message.Type, context.Message.UserId, context.Message.Message));
        }
    }
}
