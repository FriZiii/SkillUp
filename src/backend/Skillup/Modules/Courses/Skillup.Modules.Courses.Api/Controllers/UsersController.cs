using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class UsersController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPut("{UserId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditUser(EditUserRequest request)
        {
            request.UserId = request.UserId;
            await _mediator.Send(request);
            return Ok(request);
        }

        [HttpPut("{UserId}/Privacy-Settings")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditPrivacySettings(EditUserPrivacySettingsRequest request)
        {
            request.UserId = request.UserId;
            await _mediator.Send(request);
            return Ok(request);
        }

        [HttpPut("{UserId}/Profile-Picture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditProfilePicture()
        {
            //TODO : EditProfilePicture
            return Ok();
        }
    }
}
