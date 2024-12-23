using MassTransit;
using Skillup.Modules.Mails.Core.Commands;
using Skillup.Shared.Abstractions.Events.Auth;
using IMediator = MediatR.IMediator;

namespace Skillup.Modules.Mails.Core.Consumers
{
    internal class SignedUpConsumer(IMediator mediator) : IConsumer<SignedUp>
    {
        private readonly IMediator _mediator = mediator;

        public async Task Consume(ConsumeContext<SignedUp> context)
        {
            try
            {
                await _mediator.Send(new AccountActivationRequest(context.Message.UserId, context.Message.Email, context.Message.ActivationToken, context.Message.TokenExpiration));
                await _mediator.Send(new AddUserRequest(context.Message.UserId, context.Message.Email, context.Message.AllowMarketingEmails));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
