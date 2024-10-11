using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Modules.Courses.Core.Requests.Queries;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class SectionsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSection(AddSectionRequest request)
        {
            await _mediator.Send(request);

            var section = await _mediator.Send(new GetSectionByIdRequest(request.SectionId));

            return Ok(section);
        }

        [HttpGet("{courseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSectionsByCourseId(Guid courseId)
        {
            var sections = await _mediator.Send(new GetSectionsRequest(courseId));
            return Ok(sections);
        }
    }
}
