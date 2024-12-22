using MassTransit;
using MediatR;
using Skillup.Modules.Mails.Core.Commands;
using Skillup.Shared.Abstractions.Events.Auth;

namespace Skillup.Modules.Mails.Core.Consumers
{
    internal class PasswordChangedConsumer(IMediator mediator) : IConsumer<PasswordChanged>
    {
        private readonly IMediator _mediator = mediator;

        public async Task Consume(ConsumeContext<PasswordChanged> context)
        {
            await _mediator.Send(new PasswordChangedRequest(context.Message.UserId));
        }
    }
}
