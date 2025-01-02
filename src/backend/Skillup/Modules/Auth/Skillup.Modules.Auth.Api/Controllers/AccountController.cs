using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Auth.Api.Controllers.Base;
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

            HttpContext.Response.Cookies.Append("refreshToken", tokens.RefreshToken.Token, new CookieOptions
            {
                Expires = DateTimeOffset.FromUnixTimeMilliseconds(tokens.RefreshToken.Expiry),
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });

            return Ok(new { tokens.AccessToken.Token });
        }

        [HttpDelete("sign-out")]
        [SwaggerOperation("Sign out")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async new Task<IActionResult> SignOut()
        {
            //await _mediator.Send(new SignOutRequest(new Guid()));
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
