using MassTransit.Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Application.Managments.Course;
using Skillup.Modules.Courses.Application.Managments.Course.Commands;
using Skillup.Modules.Courses.Core.Entities;
using Skillup.Modules.Courses.Core.Interfaces;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator, ICourseRepository courseRepository, ICategoryRepository categoryRepository, ISubcategoryRepository subcategoryRepository)
        {
            _courseRepository = courseRepository;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(AddCourse addCourse)
        {
            /// <summary>
            ///var category = (await _categoryRepository.GetAll()).FirstOrDefault();
            ///var subcategory = (await _subcategoryRepository.GetAll()).FirstOrDefault();
            ///var category = new Category() { Name = "Programowanie" };
            ///var subcategory = new Subcategory() { Name = "C#" };

            ///var course = new Course()
            ///{
            ///    Info = new CourseInfo()
            ///    {
            ///        Title = "Nauka c#",
            ///        Subtitle = "dogłębna nauka programowania",
            ///        Description = "w tym kursie nauczysz sie roznych rzeczy"
            ///    },
            ///    Category = category,
            ///    Subcategory = subcategory,
            ///    Level = CourseLevel.Beginner,
            ///    ObjectivesSummary = new StringList(new List<string> { "nauczysz sie pisać w c#", "nauczysz sie uzywac vs", "nauczysz sie programowania obiektowego" }),
            ///    MustKnowBefore = new StringList(new List<string> { "nauczysz sie pisać w c#", "nauczysz sie uzywac vs", "nauczysz sie programowania obiektowego" }),
            ///    IntendedFor = new StringList(new List<string> { "nauczysz sie pisać w c#", "nauczysz sie uzywac vs", "nauczysz sie programowania obiektowego" }),
            ///    ThumbnailUrl = new Uri("https://cdn.pixabay.com/photo/2023/02/04/00/01/ai-generated-7766114_1280.jpg"),
            ///    Price = new Price(999),
            ///};
            ///await _courseRepository.Add(course);
            /// </summary>
            await _mediator.Send(addCourse);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _courseRepository.GetAll();
            return Ok(courses);
        }
    }
}
