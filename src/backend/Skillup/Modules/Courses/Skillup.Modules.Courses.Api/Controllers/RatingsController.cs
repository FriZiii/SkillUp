using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands.Ratings;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class RatingsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpPost]
        [Route("/Courses/{courseId}/Ratings")]
        [SwaggerOperation("Add rating")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRating(AddCourseRatingRequest request, Guid courseId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            request.UserId = (Guid)userId;
            request.CourseId = courseId;

            await _mediator.Send(request);

            return Ok(await _mediator.Send(new GetCourseRatingByIdRequest(request.RatingId)));
        }

        [HttpPatch("{ratingId}")]
        [SwaggerOperation("Edit rating")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditRating(Guid ratingId, EditCourseRatingRequest request)
        {
            request.RatingId = ratingId;
            await _mediator.Send(request);

            return Ok(await _mediator.Send(new GetCourseRatingByIdRequest(ratingId)));
        }


        [HttpDelete("{ratingId}")]
        [SwaggerOperation("Delete rating")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteRating(Guid ratingId)
        {
            await _mediator.Send(new DeleteCourseRatingRequest(ratingId));
            return Ok();
        }

        [HttpGet]
        [SwaggerOperation("Get ratings")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAverageRatings()
        {
            return Ok(await _mediator.Send(new GetAverageRatingsRequest()));
        }

        [HttpGet]
        [Route("/Courses/{courseId}/Ratings")]
        [SwaggerOperation("Get ratings by course id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRatingsByCourseId(Guid courseId, [FromQuery] int? latestUserRatingsCount) // if latestRatingsCount == null, return all UserRatings by this course
        {
            return Ok(await _mediator.Send(new GetRatingsByCourseIdRequest(courseId, latestUserRatingsCount)));
        }

        [HttpGet("{userId}")]
        [SwaggerOperation("Get ratings by user id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRatingsByUserId(Guid userId)
        {
            return Ok(await _mediator.Send(new GetRatingsByUserIdRequest(userId)));
        }
    }
}
