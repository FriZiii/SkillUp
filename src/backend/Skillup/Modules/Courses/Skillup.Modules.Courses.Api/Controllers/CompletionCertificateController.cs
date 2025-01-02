
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Courses.Core.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Courses.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CompletionCertificateController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("/Courses/{courseId}/CompletionCertificate")]
        [SwaggerOperation("Get completion certificate")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid courseId)
        {
            //var userId = User.GetUserId();
            //if (userId == null) return Unauthorized();
            //request.UserId = (Guid)userId;


            var response = await _mediator.Send(new GetCompletionCertificateRequest(Guid.NewGuid(), Guid.NewGuid()));
            return File(response.FileData, response.ContentType, response.FileName);
        }
    }
}
