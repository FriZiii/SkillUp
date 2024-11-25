using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CoursesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [SwaggerOperation("Add course")]
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [Route("/Courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AddCourseRequest request)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            request.AuthorId = (Guid)userId;
            await _mediator.Send(request);

            var course = await _mediator.Send(new GetCourseByIdRequest(request.CourseID));

            return Ok(course);
        }

        [HttpPatch]
        [Authorize(Roles = nameof(UserRole.Instructor))]
        [SwaggerOperation("Publish course")]
        [Route("/Courses/{courseId}/Publish")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Publish(Guid courseId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            await _mediator.Send(new PublishCourseRequest(courseId));

            var course = await _mediator.Send(new GetCourseByIdRequest(courseId));
            //TODO: add possibility to unpublish a course
            return Ok(course);
        }

        [HttpGet]
        [Route("/Courses")]
        [SwaggerOperation("Get courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _mediator.Send(new GetCoursesRequest());
            return Ok(courses);
        }

        [HttpGet]
        [SwaggerOperation("Get course by id")]
        [Route("/Courses/{courseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid courseId)
        {
            var course = await _mediator.Send(new GetCourseByIdRequest(courseId));
            return Ok(course);
        }

        [HttpPut]
        [SwaggerOperation("Edit course")]
        [Route("/Courses/{courseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditCourse(Guid courseId, EditCourseRequest request)
        {
            request.CourseID = courseId;
            var course = await _mediator.Send(request);
            return Ok(course);
        }

    }
}
