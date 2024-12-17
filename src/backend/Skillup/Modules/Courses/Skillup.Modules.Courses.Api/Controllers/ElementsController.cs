using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ElementsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

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

        [HttpPut("{elementId}/{newIndex}")]
        [SwaggerOperation("Change element index")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeElementIndex(Guid elementId, int newIndex)
        {
            var section = await _mediator.Send(new EditElementIndexRequest(elementId, newIndex));
            return Ok(section);
        }

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

        [HttpDelete("{elementId}")]
        [SwaggerOperation("Delete element")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteElement(Guid elementId)
        {
            await _mediator.Send(new DeleteElementRequest(elementId));
            return Ok();
        }


        [HttpPost("Attachments/{elementId}")]
        [SwaggerOperation("Add attachment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAttachment(Guid elementId, IFormFile file)
        {
            var request = new AddAttachmentRequest(file, elementId);

            await _mediator.Send(request);

            return Ok(request.AttachmentId);
        }

        [HttpGet("Attachments/{attachmentId}")]
        [SwaggerOperation("Get attachment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttachment(Guid attachmentId)
        {
            var response = await _mediator.Send(new GetAttachmentRequest(attachmentId));
            return File(response.FileData, response.ContentType, response.FileName);
        }

        [HttpGet("{elementId}/Attachments")]
        [SwaggerOperation("Get attachments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttachmentByElementId(Guid elementId)
        {
            return Ok(await _mediator.Send(new GetAttachmentsByElementIdRequest(elementId)));
        }

        [HttpDelete("Attachments/{attachmentId}")]
        [SwaggerOperation("Delete attachment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAttachment(Guid attachmentId)
        {
            await _mediator.Send(new DeleteAttachmentRequest(attachmentId));
            return Ok();
        }
    }
}
