using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Auth.Api.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Auth.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class PasswordController : BaseController
    {
        [Authorize]
        [HttpPut]
        [SwaggerOperation("Change password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Change()
        {
            return NoContent();
        }
    }
}
