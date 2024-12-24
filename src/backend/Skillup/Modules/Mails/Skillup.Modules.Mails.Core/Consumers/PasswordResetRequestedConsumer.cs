using MassTransit;
using MediatR;
using Skillup.Modules.Mails.Core.Commands;
using Skillup.Shared.Abstractions.Events.Auth;

namespace Skillup.Modules.Mails.Core.Consumers
{
    internal class PasswordResetRequestedConsumer(IMediator mediator) : IConsumer<PasswordResetRequested>
    {
        private readonly IMediator _mediator = mediator;

        public async Task Consume(ConsumeContext<PasswordResetRequested> context)
        {
            await _mediator.Send(new PasswordResetRequest(context.Message.UserId, context.Message.Token));
        }
    }
}
