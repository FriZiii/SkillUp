using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class SectionsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("{courseId}")]
        [SwaggerOperation("Add section")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSection(Guid courseId, AddSectionRequest request)
        {
            request.CourseId = courseId;
            await _mediator.Send(request);

            var section = await _mediator.Send(new GetSectionByIdRequest(request.SectionId));

            return Ok(section);
        }

        [HttpGet("{courseId}")]
        [SwaggerOperation("Get sections by course id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSectionsByCourseId(Guid courseId)
        {
            var sections = await _mediator.Send(new GetSectionsRequest(courseId));
            return Ok(sections);
        }
    }
}
