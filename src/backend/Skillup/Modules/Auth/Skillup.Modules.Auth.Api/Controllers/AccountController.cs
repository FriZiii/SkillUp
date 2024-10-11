using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Skillup.Modules.Auth.Api.Controllers.Base;
using Skillup.Modules.Auth.Core.DTO;
using Skillup.Modules.Auth.Core.Features.Commands.Account;
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
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpPost("sign-in")]
        [SwaggerOperation("Sign in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            await _mediator.Send(request);
            var tokens = _authTokenStorage.GetTokens(request.Id);

            return Ok(new TokensDto(tokens));
        }

        [Authorize]
        [HttpDelete("sign-out")]
        [SwaggerOperation("Sign out")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async new Task<IActionResult> SignOut()
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            await _mediator.Send(new SignOutRequest(Guid.Parse(userIdClaim.Value)));
            return NoContent();
        }

        [HttpPut("activation")]
        [SwaggerOperation("Activation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Activation([FromQuery] Guid userId, [FromQuery] Guid activationToken)
        {
            await _mediator.Send(new AccountActivationRequest(userId, activationToken));
            return NoContent();
        }
    }
}
