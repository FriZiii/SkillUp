using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Auth.Api.Controllers.Base;
using Skillup.Modules.Auth.Core.Commands;
using Skillup.Modules.Auth.Core.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class AccountController(IMediator mediator, IAuthTokenStorage authTokenStorage) : BaseController
    {
        private readonly IMediator _mediator = mediator;
        private readonly IAuthTokenStorage _authTokenStorage = authTokenStorage;

        [HttpPost("sign-up")]
        [SwaggerOperation("Sign up")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp(SignUp command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost("sign-in")]
        [SwaggerOperation("Sign in")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(SignIn command)
        {
            await _mediator.Send(command);
            var tokens = _authTokenStorage.GetTokens(command.Id);
            return Ok(new { tokens.AccessToken, tokens.RefreshToken });
        }

        [Authorize]
        [HttpDelete("sign-out")]
        [SwaggerOperation("Sign out")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SignOut()
        {
            await _mediator.Send(new SignOut(new Guid(User.Claims.First(x => x.Type.Equals("Id")).Value)));
            return NoContent();
        }

        [HttpPut("activation")]
        [SwaggerOperation("Activation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Activation([FromQuery] Guid userId, [FromQuery] Guid activationToken)
        {
            await _mediator.Send(new AccountActivation(userId, activationToken));
            return Ok();
        }
    }
}
