using MassTransit;
using MediatR;
using Skillup.Shared.Abstractions.Events.Finances;

namespace Skillup.Modules.Courses.Infrastracture.Consumers
{
    internal class CoursePurchasedConsumer : IConsumer<CoursePurchased>
    {
        private readonly IMediator _mediator;

        public CoursePurchasedConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task Consume(ConsumeContext<CoursePurchased> context)
        {
            throw new NotImplementedException();
        }
    }
}
