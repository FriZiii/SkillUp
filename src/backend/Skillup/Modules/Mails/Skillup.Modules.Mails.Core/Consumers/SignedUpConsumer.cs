using MassTransit;
using Skillup.Modules.Mails.Core.Commands;
using Skillup.Shared.Abstractions.Events.Auth;
using IMediator = MediatR.IMediator;

namespace Skillup.Modules.Mails.Core.Consumers
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
            await _mediator.Send(new AccountActivation(context.Message.Email));
        }
    }
}
