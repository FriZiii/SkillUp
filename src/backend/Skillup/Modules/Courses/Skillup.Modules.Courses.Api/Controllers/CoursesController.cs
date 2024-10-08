﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Application.Operations.Commands.AddCourse;
using Skillup.Modules.Courses.Application.Operations.Commands.AddDetails;
using Skillup.Modules.Courses.Application.Operations.Commands.AddSection;
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
            await _mediator.Send(addCourse);
            return Ok(addCourse);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            var category = new Core.Entities.Category() { Name = "Programowanie" };
            await _categoryRepository.Add(category);

            await _subcategoryRepository.Add(new Core.Entities.Subcategory() { Name = "C#", CategoryId = category.Id });
            //var courses = await _courseRepository.GetAll();
            return Ok();
        }

        [HttpPost("details")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddDetails(AddDetails addDetails)
        {
            await _mediator.Send(addDetails);
            return Ok(addDetails);
        }

        [HttpPost("section")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddSection(AddSection addSection)
        {
            await _mediator.Send(addSection);
            return Ok(addSection);
        }
    }
}