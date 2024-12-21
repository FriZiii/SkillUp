using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands.Comments;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CommentsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpGet("/Courses/Elements/{elementId}/Comments")]
        [SwaggerOperation("Get comments for element")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid elementId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            return Ok(await _mediator.Send(new GetCommentsByElementIdRequest(elementId, (Guid)userId)));
        }

        [Authorize]
        [HttpPost("/Courses/Elements/{elementId}/Comments")]
        [SwaggerOperation("Add new comment for element")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(Guid elementId, AddCommentRequest request)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            request.ElementId = elementId;
            request.UserId = (Guid)userId;

            await _mediator.Send(request);

            return Ok(await _mediator.Send(new GetCommentsByElementIdRequest(elementId, (Guid)userId)));
        }

        [Authorize]
        [HttpDelete("/Courses/Elements/Comments/{commentId}")]
        [SwaggerOperation("Delete comment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid commentId)
        {
            await _mediator.Send(new DeleteCommentRequest(commentId));
            return Ok();
        }

        [Authorize]
        [HttpPatch("/Courses/Elements/Comments/{commentId}/ToggleLike")]
        [SwaggerOperation("Toggle like for comment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ToggleLikeForComment(Guid commentId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            await _mediator.Send(new ToggleLikeForCommentRequest(commentId, (Guid)userId));

            return Ok();
        }
    }
}
