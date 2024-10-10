using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Application.Features.Commands;
using Skillup.Modules.Courses.Core.Entities.CourseEntities;
using Skillup.Modules.Courses.Core.Interfaces;
using Skillup.Modules.Courses.Core.Requests;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CoursesController(IMediator mediator, ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository = subcategoryRepository;

        [HttpPost]
        [Route("/Courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AddCourseRequest request)
        {
            await _mediator.Send(request);
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
        [Route("/Courses/ById")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid courseId)
        {
            var course = await _mediator.Send(new GetCourseByIdRequest() { CourseId = courseId});
            return Ok(course);
        }
    }
}
