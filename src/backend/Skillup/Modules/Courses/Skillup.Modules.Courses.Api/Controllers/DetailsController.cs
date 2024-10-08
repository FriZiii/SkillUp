using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Application.Operations.Commands.AddDetails;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class DetailsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDetails(AddDetails addDetails)
        {
            await _mediator.Send(addDetails);
            return Ok(addDetails);
        }
    }
}
