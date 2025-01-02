using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands.Sections;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class SectionsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = nameof(UserRole.Instructor))]
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

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPut("{sectionId}/{newIndex}")]
        [SwaggerOperation("Change section index")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeSectionIndex(Guid sectionId, int newIndex)
        {
            var sections = await _mediator.Send(new EditSectionIndexRequest(sectionId, newIndex));
            return Ok(sections);
        }

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPut("{sectionId}")]
        [SwaggerOperation("Edit section")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditSection(Guid sectionId, EditSectionRequest request)
        {
            request.SectionId = sectionId;
            await _mediator.Send(request);
            return Ok();
        }

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpDelete("{sectionId}")]
        [SwaggerOperation("Delete section")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteSection(Guid sectionId)
        {
            await _mediator.Send(new DeleteSectionRequest { SectionId = sectionId });
            return Ok();
        }
    }
}
