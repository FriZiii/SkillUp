using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Chat.Core.Features;
using Skillup.Shared.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Chat.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ChatController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpGet("/Chats/Users/{userId}")]
        [SwaggerOperation("Get chats by userId")]
        public async Task<IActionResult> GetChatsByUser(Guid userId)
        {
            return Ok(await _mediator.Send(new GetChatsByUserRequest(userId)));
        }

        [Authorize]
        [HttpGet("/Chats/{chatId}/Messages")]
        [SwaggerOperation("Get messages by chatId")]
        public async Task<IActionResult> GetMessages(Guid chatId)
        {
            var userId = User.GetUserId();
            if (userId is null)
                return Unauthorized();

            return Ok(await _mediator.Send(new GetMessagesRequest(chatId, (Guid)userId)));
        }
    }
}
