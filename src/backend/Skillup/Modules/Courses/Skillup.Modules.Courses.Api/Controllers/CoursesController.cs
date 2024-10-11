﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Application.Features.Commands;
using Skillup.Modules.Courses.Core.Requests;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CoursesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        //[Authorize]
        [Route("/Courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AddCourseRequest request)
        {
            //TODO : User claims
            //var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub);
            //if (userIdClaim == null)
            //{
            //    return Unauthorized();
            //}
            //request.AuthorId = Guid.Parse(userIdClaim.Value);
            await _mediator.Send(request);

            await _mediator.Send(new GetCourseByIdRequest(request.CourseID));

            return Ok();
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
