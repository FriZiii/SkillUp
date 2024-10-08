using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Auth.Api.Controllers.Base;
using Skillup.Modules.Auth.Core.Commands.Token;
using Skillup.Modules.Auth.Core.DTO;
using Skillup.Modules.Auth.Core.Services;
using Swashbuckle.AspNetCore.Annotations;
using IMediator = MediatR.IMediator;

namespace Skillup.Modules.Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class TokenController(IMediator mediator, IAuthTokenStorage authTokenStorage) : BaseController
    {
        private readonly IMediator _mediator = mediator;
        private readonly IAuthTokenStorage _authTokenStorage = authTokenStorage;

        [HttpPost("refresh")]
        [SwaggerOperation("Refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken()
        {
            var accessToken = Request.Headers.Authorization.ToString()["Bearer ".Length..].Trim();
            var refreshToken = Request.Cookies["refreshToken"];

            var command = new Refresh(accessToken, refreshToken);
            await _mediator.Send(command);

            var tokens = _authTokenStorage.GetTokens(command.Id);

            return Ok(new TokensDto(tokens));
        }
    }
}
