using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Mails.Core.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Mails.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{userId}")]
        [SwaggerOperation("Get mail information by user id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return Ok(await _mediator.Send(new GetUserRequest(userId)));
        }

        [HttpPut("{userId}")]
        [SwaggerOperation("Edit mail information by user id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUser(Guid userId, EditUserRequest request)
        {
            request.UserId = userId;
            await _mediator.Send(request);

            return Ok();
        }
    }
}
