using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Notifications.Core.Features.Requests;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Notifications.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class NotificationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("/Notifications/Users/{userId}")]
        [SwaggerOperation("Get notifications by user id")]
        public async Task<IActionResult> GetNotificationsByUserId(Guid userId)
        {
            return Ok(await _mediator.Send(new GetNotificationsByUserIdRequest(userId)));
        }

        [HttpPatch("/Notifications/{notificationId}")]
        [SwaggerOperation("Mark notification as seen")]
        public async Task<IActionResult> MarkNotificationAsSeen(Guid notificationId)
        {
            await _mediator.Send(new MarkNotificationAsSeenRequest(notificationId));
            return Ok();
        }
    }
}
