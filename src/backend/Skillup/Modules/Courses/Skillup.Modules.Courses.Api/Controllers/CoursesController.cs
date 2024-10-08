using MediatR;
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
            //var category = new Core.Entities.Category() { Name = "Programowanie" };
            //await _categoryRepository.Add(category);

            //await _subcategoryRepository.Add(new Core.Entities.Subcategory() { Name = "C#", CategoryId = category.Id });

            var courses = await _mediator.Send(new GetCoursesRequest());
            return Ok(courses);
        }
    }
}
