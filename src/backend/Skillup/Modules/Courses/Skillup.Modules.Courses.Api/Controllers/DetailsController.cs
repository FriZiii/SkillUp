using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Commands;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class DetailsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        [SwaggerOperation("Edit courses details")]
        [HttpPut("/Courses/{courseId}/Details")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditDetails(Guid courseId, EditDetailsRequest request)
        {
            request.CourseId = courseId;
            await _mediator.Send(request);
            return Ok();
        }

        [SwaggerOperation("Edit courses tumbnail")]
        [HttpPut("/Courses/{courseId}/Details/TumbnailPicture")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditTumbnailPicture(Guid courseId, IFormFile file)
        {
            await _mediator.Send(new EditCourseTumbnailRequest(courseId, file));

            return Ok(await _mediator.Send(new GetCourseByIdRequest(courseId)));
        }
    }
}
