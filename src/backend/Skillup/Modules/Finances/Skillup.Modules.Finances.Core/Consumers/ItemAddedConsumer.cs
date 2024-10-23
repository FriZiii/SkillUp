using MassTransit;
using MediatR;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Shared.Abstractions.Events.Finances;

namespace Skillup.Modules.Finances.Core.Consumers
{
    internal class ItemAddedConsumer : IConsumer<IItemAdded>
    {
        private readonly IMediator _mediator;

        public ItemAddedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<IItemAdded> context)
        {
            await _mediator.Send(new AddItemRequest(context.Message.ItemId, context.Message.AuthorId, context.Message.Type));
        }
    }
}
