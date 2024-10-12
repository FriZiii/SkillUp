using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Auth.Api.Controllers.Base;
using Skillup.Modules.Auth.Core.Features.Requests.User;
using System.IdentityModel.Tokens.Jwt;

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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangeUserRole(ChangeUserRoleRequest request)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            request.RequestingUserId = Guid.Parse(userIdClaim.Value);

            await _mediator.Send(request);

            return Ok();
        }

        [Authorize]
        [HttpPatch("user-state")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangeUserState(ChangeUserStateRequest request)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            request.RequestingUserId = Guid.Parse(userIdClaim.Value);

            await _mediator.Send(request);

            return Ok();
        }
    }
}
