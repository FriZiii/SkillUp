﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Application.Features.Queries;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
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

        [HttpGet]
        [Route("/Courses")]
        [SwaggerOperation("Get courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll([FromQuery] CourseStatus? status)
        {
            var courses = await _mediator.Send(new GetCoursesRequest(status));
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

        [HttpGet]
        [SwaggerOperation("Get course by author id")]
        [Route("/Courses/Author/{authorId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByAuthorId(Guid authorId, [FromQuery] CourseStatus? status)
        {
            var courses = await _mediator.Send(new GetCoursesByAuthorIdRequest(authorId, status));
            return Ok(courses);
        }

        [Authorize]
        [HttpGet]
        [SwaggerOperation("Get course purchased by user")]
        [Route("/Courses/UserId/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var course = await _mediator.Send(new GetCourseByUserIdRequest(userId));
            return Ok(course);
        }

        [Authorize(Roles = nameof(UserRole.Instructor))]
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
