using MassTransit;
using MediatR;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Shared.Abstractions.Events.Auth;

namespace Skillup.Modules.Finances.Core.Consumers
{
    internal class SignedUpConsumer : IConsumer<SignedUp>
    {
        private readonly IMediator _mediator;

        public SignedUpConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<SignedUp> context)
        {
            await _mediator.Send(new CreateUserRequest(context.Message.UserId));
        }
    }
}
