using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Auth.Api.Controllers.Base;
using Skillup.Modules.Auth.Core.Features.Requests.User;
using Skillup.Shared.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPatch("user-role")]
        [SwaggerOperation("Change user role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangeUserRole(ChangeUserRoleRequest request)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            request.RequestingUserId = (Guid)userId;

            await _mediator.Send(request);

            return Ok();
        }

        [Authorize]
        [HttpPatch("user-state")]
        [SwaggerOperation("Change user state")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangeUserState(ChangeUserStateRequest request)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            request.RequestingUserId = (Guid)userId;

            await _mediator.Send(request);

            return Ok();
        }
    }
}
