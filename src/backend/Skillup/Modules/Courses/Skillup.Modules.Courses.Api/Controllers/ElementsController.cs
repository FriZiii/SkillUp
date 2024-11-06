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

        [HttpPut("{elementId}")]
        [SwaggerOperation("Change element index")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ChangeSectionIndex(Guid elementId, EditElementIndexRequest request)
        {
            request.ElementId = elementId;
            var elements = await _mediator.Send(request);
            return Ok(elements);
        }
    }
}
