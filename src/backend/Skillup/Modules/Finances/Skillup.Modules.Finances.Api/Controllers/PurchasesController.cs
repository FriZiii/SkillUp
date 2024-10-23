using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Shared.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Finances.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class PurchasesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchasesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [SwaggerOperation("Purchase item")]
        [HttpPost("{itemId}")]
        public async Task<IActionResult> PurchaseItem(Guid itemId)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            await _mediator.Send(new ItemPurchaseRequest(itemId, (Guid)userId));

            return Ok();
        }

        [Authorize]
        [SwaggerOperation("Get purchases histories by item id")]
        [HttpGet("History/Item/{itemId}")]
        public async Task<IActionResult> GetPurchasesHistoryByItemId(Guid itemId)
        {
            var histories = await _mediator.Send(new GetPurchaseHistoriesByItemIdRequest(itemId));
            return Ok(histories);
        }

        [Authorize]
        [SwaggerOperation("Get purchases histories by author id")]
        [HttpGet("History/Author/{authorId}")]
        public async Task<IActionResult> GetPurchasesHistoryByAuthorId(Guid authorId)
        {
            var histories = await _mediator.Send(new GetPurchaseHistoriesByAuthorIdRequest(authorId));
            return Ok(histories);
        }

        [Authorize]
        [SwaggerOperation("Get purchases histories by user id")]
        [HttpGet("History/User/{userId}")]
        public async Task<IActionResult> GetPurchasesHistoryByUserId(Guid userId)
        {
            var histories = await _mediator.Send(new GetPurchaseHistoriesByUserIdRequest(userId));
            return Ok(histories);
        }
    }
}
