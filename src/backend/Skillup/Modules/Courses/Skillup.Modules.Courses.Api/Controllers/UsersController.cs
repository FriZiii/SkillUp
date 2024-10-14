using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{userId}")]
        [SwaggerOperation("Get user by id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUserById(Guid userId, [FromQuery] bool details = false)
        {
            return Ok(await _mediator.Send(new GetUserByIdRequest(userId, details)));
        }

        [HttpPut("{userId}")]
        [SwaggerOperation("Edit user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUser(Guid userId, EditUserRequest request)
        {
            request.UserId = userId;
            await _mediator.Send(request);

            return Ok();
        }

        [HttpPut("{userId}/Privacy-Settings")]
        [SwaggerOperation("Edit privacy settings")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditPrivacySettings(Guid userId, EditUserPrivacySettingsRequest request)
        {
            request.UserId = userId;
            await _mediator.Send(request);
            return Ok(request);
        }

        [HttpPut("{UserId}/Profile-Picture")]
        [SwaggerOperation("Edit profile picture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditProfilePicture()
        {
            //TODO : EditProfilePicture
            return Ok();
        }
    }
}
