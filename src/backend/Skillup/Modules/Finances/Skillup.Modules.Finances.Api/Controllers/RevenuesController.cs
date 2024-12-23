using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Finances.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class RevenuesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("Author/{authorId}/per-courses")]
        [SwaggerOperation("Get ernings and sales per course")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEarningsAndSalesPerCourse(Guid authorId)
        {
            return Ok(await _mediator.Send(new GetEarningsAndSalesPerCourseByAuthorIdRequest(authorId)));
        }

        [HttpGet("Author/{authorId}/monthly")]
        [SwaggerOperation("Get monthly earnings per courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEarningsAndSalesPerCourse(Guid authorId, [FromQuery] int year)
        {
            return Ok(await _mediator.Send(new GetMonthlyEarningsPerCoursesByAuthorIdRequest(authorId, year)));
        }

        [HttpGet("Author/{authorId}")]
        [SwaggerOperation("Get revenue")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRevenue(Guid authorId)
        {
            return Ok(await _mediator.Send(new GetRevenueByAuthorIdRequest(authorId)));
        }
    }
}
