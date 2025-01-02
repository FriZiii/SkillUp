using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ElementsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPost("{assetType}/{sectionId}")]
        [SwaggerOperation("Add element")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddElement(Guid sectionId, [FromRoute] AssetType assetType, AddElementRequest request)
        {
            request.SectionId = sectionId;
            request.AssetType = assetType;
            await _mediator.Send(request);

            var element = await _mediator.Send(new GetElementByIdRequest(request.ElementId));

            return Ok(element);
        }

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPut("{elementId}/{newIndex}")]
        [SwaggerOperation("Change element index")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeElementIndex(Guid elementId, int newIndex)
        {
            var section = await _mediator.Send(new EditElementIndexRequest(elementId, newIndex));
            return Ok(section);
        }

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpPut("{elementId}")]
        [SwaggerOperation("Edit element")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditElement(Guid elementId, EditElementRequest request)
        {
            request.ElementId = elementId;
            await _mediator.Send(request);
            return Ok();
        }

        [Authorize(Roles = nameof(UserRole.Instructor))]
        [HttpDelete("{elementId}")]
        [SwaggerOperation("Delete element")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteElement(Guid elementId)
        {
            await _mediator.Send(new DeleteElementRequest(elementId));
            return Ok();
        }
    }
}
