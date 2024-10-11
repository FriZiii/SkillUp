using MassTransit;
using MediatR;
using Skillup.Modules.Courses.Core.Requests;
using Skillup.Shared.Abstractions.Events.Auth;

namespace Skillup.Modules.Courses.Infrastracture.Consumers
{
    internal class SignedUpConsumer(IMediator mediator) : IConsumer<SignedUp>
    {
        private readonly IMediator _mediator = mediator;

        public async Task Consume(ConsumeContext<SignedUp> context)
        {
            await _mediator.Send(new AddUserRequest(context.Message.UserId, context.Message.Email));
        }
    }
}
