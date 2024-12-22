using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Auth.Api.Controllers.Base;
using Skillup.Modules.Auth.Core.Features.Requests.Password;
using Skillup.Shared.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class PasswordController(IMediator mediator) : BaseController
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpPost("change")]
        [SwaggerOperation("Change password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            request.UserId = (Guid)userId;

            await _mediator.Send(request);
            return NoContent();
        }

        [HttpPost("reset/request")]
        [SwaggerOperation("Request reset password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ResetRequest([FromQuery] string email)
        {
            await _mediator.Send(new ResetPasswordRequest(email));
            return NoContent();
        }

        [HttpPost("reset/subbmit")]
        [SwaggerOperation("Reset password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ResetSubbmit(ResetPasswordSubmitRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
