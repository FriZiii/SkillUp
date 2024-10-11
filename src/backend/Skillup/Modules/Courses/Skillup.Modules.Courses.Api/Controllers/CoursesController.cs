using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Modules.Courses.Core.Requests.Queries;
using System.IdentityModel.Tokens.Jwt;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CoursesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Authorize]
        [Route("/Courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AddCourseRequest request)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            request.AuthorId = Guid.Parse(userIdClaim.Value);
            await _mediator.Send(request);

            var course = await _mediator.Send(new GetCourseByIdRequest(request.CourseID));

            return Ok(course);
        }

        [HttpPatch]
        [Authorize]
        [Route("/Courses/{courseId}/Publish")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Publish(Guid courseId)
        {
            var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            await _mediator.Send(new PublishCourseRequest(courseId));

            var course = await _mediator.Send(new GetCourseByIdRequest(courseId));
            //TODO: add possibility to unpublish a course
            return Ok(course);
        }

        [HttpGet]
        [Route("/Courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _mediator.Send(new GetCoursesRequest());
            return Ok(courses);
        }

        [HttpGet]
        [Route("/Courses/{courseId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid courseId)
        {
            var course = await _mediator.Send(new GetCourseByIdRequest(courseId));
            return Ok(course);
        }
    }
}
