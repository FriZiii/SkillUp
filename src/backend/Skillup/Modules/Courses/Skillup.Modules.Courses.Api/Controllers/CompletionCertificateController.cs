
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

            var cid = new Guid("001ad0d5-9230-4bd1-be76-2ffcb72507ba");
            var userId = new Guid("03b23b0c-383a-4e7b-9714-35eb69e4918d");

            var response = await _mediator.Send(new GetCompletionCertificateRequest(cid, userId));
            return File(response.FileData, response.ContentType, response.FileName);
        }
    }
}
