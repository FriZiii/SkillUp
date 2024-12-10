using MassTransit;
using MediatR;
using Skillup.Modules.Courses.Core.Requests.Commands.Users;
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

        public async Task Consume(ConsumeContext<CoursePurchased> context)
        {
            await _mediator.Send(new AddUserPurchasedCourseRequest(context.Message.CourseId, context.Message.UserId));
        }
    }
}
