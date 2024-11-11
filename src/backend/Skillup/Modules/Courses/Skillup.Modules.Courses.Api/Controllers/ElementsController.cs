using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ElementsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("article")]
        [SwaggerOperation("Add article")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddArticle(AddArticleRequest request)
        {
            await _mediator.Send(request);
            return Ok(request);
        }

        [HttpPut("{elementId}/Edit-Index")]
        [SwaggerOperation("Change element index")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeElementIndex(Guid elementId, EditElementIndexRequest request)
        {
            request.ElementId = elementId;
            var section = await _mediator.Send(request);
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
            await _mediator.Send(new DeleteElementRequest { ElementId = elementId });
            return Ok();
        }
    }
}
