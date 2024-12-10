using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Modules.Courses.Core.Requests.Commands.Review;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ReviewController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPatch]
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [SwaggerOperation("Submit to review")]
        [Route("/Courses/{courseId}/Submit")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SubmitToReview(Guid courseId)
        {
            await _mediator.Send(new EditCourseStatusRequest(courseId, CourseStatus.SubmitedForReview));

            var course = await _mediator.Send(new GetCourseByIdRequest(courseId));
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [SwaggerOperation("Start review")]
        [Route("/Courses/{courseId}/Reviews")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> StarReview(Guid courseId)
        {
            var request = new StartReviewRequest(courseId);
            await _mediator.Send(new StartReviewRequest(courseId));
            return Ok(await _mediator.Send(new GetReviewByIdRequest(request.ReviewId)));
        }

        [HttpPatch]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [SwaggerOperation("Finalize review")]
        [Route("{reviewId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FinalizeReview(Guid reviewId, [FromQuery] ReviewStatus reviewStatus)
        {
            await _mediator.Send(new FinalizeReviewRequest(reviewId, reviewStatus));
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [SwaggerOperation("Get reviews with status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetReviewesWithStatus([FromQuery] ReviewStatus reviewStatus)
        {
            return Ok(await _mediator.Send(new GetReviewsWithStatusRequest(reviewStatus)));
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("Get reviews by course")]
        [Route("/Courses/{courseId}/Reviews")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetReviewsByCurse(Guid courseId)
        {
            return Ok(await _mediator.Send(new GetReviewsByCourseIdRequest(courseId)));
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation("Get review")]
        [Route("/Courses/{courseId}/Reviews/Latest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLatestReviewByCourse(Guid courseId)
        {
            return Ok(await _mediator.Send(new GetLatestReviewByCourseIdRequest(courseId)));
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [SwaggerOperation("Add comment")]
        [Route("{reviewId}/Comments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddComment(Guid reviewId, [FromQuery] Guid elementId, string comment)
        {
            await _mediator.Send(new AddReviewCommentRequest(reviewId, elementId, comment));
            return Ok(await _mediator.Send(new GetReviewByIdRequest(reviewId)));
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [SwaggerOperation("Delete comment")]
        [Route("Comments/{commentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteComment(Guid commentId)
        {
            await _mediator.Send(new DeleteReviewCommentRequest(commentId));
            return Ok();
        }

        [HttpPatch]
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [SwaggerOperation("Resolve comment")]
        [Route("Comments/{commentId}/Resolve")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResolveComment(Guid commentId)
        {
            var request = new ResolveCommentRequest(commentId);
            await _mediator.Send(request);
            return Ok(await _mediator.Send(new GetReviewByIdRequest(request.ReviewId)));
        }
    }
}
