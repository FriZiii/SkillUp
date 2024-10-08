using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Application.Operations.Commands.AddCourse;
using Skillup.Modules.Courses.Application.Operations.Queries.GetCourses;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CoursesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Route("/Courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AddCourse addCourse)
        {
            await _mediator.Send(addCourse);
            return Ok(addCourse);
        }

        [HttpGet]
        [Route("/Courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            //var category = new Core.Entities.Category() { Name = "Programowanie" };
            //await _categoryRepository.Add(category);

            //await _subcategoryRepository.Add(new Core.Entities.Subcategory() { Name = "C#", CategoryId = category.Id });

            var courses = await _mediator.Send(new GetCourses());
            return Ok(courses);
        }
    }
}
