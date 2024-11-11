using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class AssetsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("video/{elementId}")]
        [SwaggerOperation("Add video")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddVideo(Guid elementId, IFormFile file)
        {
            await _mediator.Send(new AddVideoAssetRequest(elementId, file));
            return Ok();
        }

        [HttpPost("article/{elementId}")]
        [SwaggerOperation("Add article")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddArticle(Guid elementId, IFormFile file)
        {
            await _mediator.Send(new AddArticleAssetRequest(elementId, file));
            return Ok();
        }

        [HttpDelete("{assetId}")]
        [SwaggerOperation("Delete asset")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsset(Guid assetId)
        {
            await _mediator.Send(new DeleteAssetRequest(assetId));
            return Ok();
        }
    }
}
