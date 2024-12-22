using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements.Attachment;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class AttachmentsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Route("/Courses/Elements/{elementId}/Attachments")]
        [SwaggerOperation("Add attachment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAttachment(Guid elementId, IFormFile file)
        {
            var request = new AddAttachmentRequest(file, elementId);
            await _mediator.Send(request);


            return Ok(await _mediator.Send(new GetAttachmentRequest(request.AttachmentId)));
        }

        [HttpGet]
        [Route("/Courses/Elements/Attachments/{attachmentId}")]
        [SwaggerOperation("Get attachment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttachment(Guid attachmentId)
        {
            var response = await _mediator.Send(new GetAttachmentFileRequest(attachmentId));
            return File(response.FileData, response.ContentType, response.FileName);
        }

        [HttpDelete]
        [Route("/Courses/Elements/Attachments/{attachmentId}")]
        [SwaggerOperation("Delete attachment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAttachment(Guid attachmentId)
        {
            await _mediator.Send(new DeleteAttachmentRequest(attachmentId));
            return Ok();
        }

        [HttpGet]
        [Route("/Courses/Elements/{elementId}/Attachments")]
        [SwaggerOperation("Get element attachments by element id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttachmentByElementId(Guid elementId)
        {
            return Ok(await _mediator.Send(new GetAttachmentsByElementIdRequest(elementId)));
        }

        [HttpGet]
        [Route("/Courses/{courseId}/Elements/Attachments")]
        [SwaggerOperation("Get elements attachments by course id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAttachmentByCourseId(Guid courseId)
        {
            return Ok(await _mediator.Send(new GetAttachmentsByCourseIdRequest(courseId)));
        }
    }
}
