using MassTransit;
using MediatR;
using Skillup.Modules.Chat.Core.Features;
using Skillup.Shared.Abstractions.Events.Finances;

namespace Skillup.Modules.Chat.Core.Consumers
{
    internal class CoursePurchasedConsumer : IConsumer<CoursePurchased>
    {
        private readonly IMediator _mediator;
        public CoursePurchasedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<CoursePurchased> context)
        {
            await _mediator.Send(new AddChatRequest(context.Message.CourseId, context.Message.UserId, context.Message.AuthorId));
        }
    }
}
