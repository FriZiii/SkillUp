using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent;
using Skillup.Modules.Courses.Core.Entities.CourseEntities.CourseContent.ElementContent.Assets;
using Skillup.Modules.Courses.Core.Requests.Commands.Assets;
using Skillup.Modules.Courses.Core.Requests.Queries.Assets;
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

        [HttpPost("assignment/{exerciseType}/{elementId}")]
        [SwaggerOperation("Add assignment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAssignment(Guid elementId, [FromRoute] ExerciseType exerciseType, string instruction)
        {
            var assignment = await _mediator.Send(new AddAssignmentAssetRequest(elementId, exerciseType, instruction));
            return Ok(assignment);
        }

        [HttpPut("assignment/{elementId}")]
        [SwaggerOperation("Edit assignment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditAssignment(Guid elementId, string instruction)
        {
            await _mediator.Send(new EditAssignmentAssetRequest(elementId, instruction));
            return Ok();
        }

        [HttpDelete("{elementId}")]
        [SwaggerOperation("Delete asset by elementId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsset(Guid elementId)
        {
            await _mediator.Send(new DeleteAssetRequest(elementId));
            return Ok();
        }

        [HttpGet("{assetType}/{elementId}")]
        [SwaggerOperation("Get asset by elementId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAssetByElementId(Guid elementId, [FromRoute] AssetType assetType)
        {
            return assetType switch
            {
                AssetType.Article => Ok(await _mediator.Send(new GetArticleAssetRequest(elementId))),
                AssetType.Video => Ok(await _mediator.Send(new GetVideoAssetRequest(elementId))),
                AssetType.Exercise => Ok(await _mediator.Send(new GetAssignmentAssetRequest(elementId))),
                _ => BadRequest(),
            };
        }
    }
}
