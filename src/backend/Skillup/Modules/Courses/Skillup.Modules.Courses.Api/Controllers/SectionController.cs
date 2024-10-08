using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Application.Operations.Commands.AddSection;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class SectionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSection(AddSection addSection)
        {
            await _mediator.Send(addSection);
            return Ok(addSection);
        }
    }
}
