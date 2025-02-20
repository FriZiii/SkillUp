﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands.Elements.Progress;
using Skillup.Modules.Courses.Core.Requests.Queries;
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
        [Authorize]
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
        [Authorize]
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

        [HttpGet]
        [Authorize]
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

        [HttpGet]
        [Authorize]
        [SwaggerOperation("Get completed elements for course")]
        [Route("/Courses/{courseId}/Progress")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByCourseId(Guid courseId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            //return all progress for a given course, for the logged-in user
            return Ok(await _mediator.Send(new GetProgressForCourseRequest((Guid)userId, courseId)));
        }
    }
}
