using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Auth.Api.Controllers.Base;
using Skillup.Modules.Auth.Core.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class AuthController(IMediator mediator) : BaseController
    {
        [HttpPost("sign-up")]
        [SwaggerOperation("Sign up")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [HttpPost("sign-in")]
        [SwaggerOperation("Sign in")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(SignIn command)
        {
            await mediator.Send(command);
            return Ok();
        }

        [Authorize]
        [HttpDelete("sign-out")]
        [SwaggerOperation("Sign out")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SignOut()
        {
            await mediator.Send(new SignOut(new Guid()));
            return NoContent();
        }
    }
}
