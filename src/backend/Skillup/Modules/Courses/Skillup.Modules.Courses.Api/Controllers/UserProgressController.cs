using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements.Progress;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class UserProgressController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.User))]
        [SwaggerOperation("Add progress")]
        [Route("/Courses/{courseId}/Elements/{elementId}/Progress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(Guid courseId, Guid elementId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            await _mediator.Send(new ToggleCourseElementProgressRequest((Guid)userId, courseId, elementId));
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.User))]
        [SwaggerOperation("Delete progress by id")]
        [Route("/Courses/{courseId}/Elements/{elementId}/Progress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid courseId, Guid elementId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            await _mediator.Send(new ToggleCourseElementProgressRequest((Guid)userId, courseId, elementId));
            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.User))]
        [SwaggerOperation("Get progress as percetage for courses by signed in user id")]
        [Route("/Courses/Progress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByUserId()
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            return Ok(await _mediator.Send(new GetProgressForUserCoursesRequest((Guid)userId)));
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.User))]
        [SwaggerOperation("Get completed elements for course")]
        [Route("/Courses/{courseId}/Progress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByCourseId(Guid courseId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            //zwracamy cały progress dla danego kursu, dla zalogowanego usera
            return Ok(await _mediator.Send(new GetProgressForCourseRequest((Guid)userId, courseId)));
        }
    }
}
