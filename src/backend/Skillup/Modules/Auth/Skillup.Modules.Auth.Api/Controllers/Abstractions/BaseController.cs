using Microsoft.AspNetCore.Mvc;

namespace Skillup.Modules.Auth.Api.Controllers.Base
{
    [ApiController]
    [Route("[controller]")]
    internal abstract class BaseController : ControllerBase
    {
        protected ActionResult<T> OkOrNotFound<T>(T model)
        {
            if (model is not null)
            {
                return Ok(model);
            }

            return NotFound();
        }
    }
}
