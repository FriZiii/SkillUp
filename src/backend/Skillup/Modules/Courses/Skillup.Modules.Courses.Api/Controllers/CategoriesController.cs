using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CategoriesController(IMediator mediator, ILogger<CategoriesController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<CategoriesController> _logger = logger;

        [HttpGet]
        [SwaggerOperation("Get categories")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {

            _logger.LogInformation("Pobieranie kategorii");
            _logger.LogDebug("Loguje Debug");
            var courses = await _mediator.Send(new GetCategoriesRequest());
            return Ok(courses);
        }
    }
}
