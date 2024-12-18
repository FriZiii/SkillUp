using MassTransit;
using MediatR;
using Skillup.Modules.Notifications.Core.Features.Requests;
using Skillup.Shared.Abstractions.Events.Auth;

namespace Skillup.Modules.Notifications.Core.Consumers
{
    internal class SignedUpConsumer(IMediator mediator) : IConsumer<SignedUp>
    {
        private readonly IMediator _mediator = mediator;

        public async Task Consume(ConsumeContext<SignedUp> context)
        {
            await _mediator.Send(new CreateUserRequest(context.Message.UserId));
        }
    }
}
