using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Shared.Infrastructure.Auth;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Finances.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class DiscountCodeController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("{type}")]
        [SwaggerOperation("Add new discount code")]
        public async Task<IActionResult> Add([FromRoute] DiscountCodeType type, AddDiscountCodeRequest request)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            request.OwnerId = (Guid)userId;

            request.Type = type;
            await _mediator.Send(request);

            return Ok(await _mediator.Send(new GetDiscountCodeByIdRequest(request.Id)));
        }

        [HttpDelete("{discountCodeId}")]
        [SwaggerOperation("Delete discount code")]
        public async Task<IActionResult> Delete(Guid discountCodeId)
        {
            await _mediator.Send(new DeleteDiscountCodeRequest(discountCodeId));
            return Ok();
        }

        [HttpPut("{discountCodeId}")]
        [SwaggerOperation("Edit discount code")]
        public async Task<IActionResult> Edit(Guid discountCodeId, EditDiscountCodeRequest request)
        {
            request.Id = discountCodeId;
            await _mediator.Send(request);
            return Ok(await _mediator.Send(new GetDiscountCodeByIdRequest(discountCodeId)));
        }

        [HttpGet]
        [SwaggerOperation("Get public discount codes")]
        public async Task<IActionResult> GetPublic()
        {
            return Ok(await _mediator.Send(new GetPublicDiscountCodesRequest()));
        }

        [HttpPost("{discountCodeId}/{itemId}")]
        [SwaggerOperation("Toggle discount code for item")]
        public async Task<IActionResult> ToggleDiscountCodeForItem(Guid discountCodeId, Guid itemId)
        {
            await _mediator.Send(new ToggleDiscountCodeForItemRequest(discountCodeId, itemId));
            return Ok();
        }


        [HttpGet("{ownerId}")]
        [SwaggerOperation("Get by owner id")]
        public async Task<IActionResult> GetByOwnerId(Guid ownerId)
        {
            return Ok(await _mediator.Send(new GetDiscountCodeByOwnerIdRequest(ownerId)));
        }

    }
}
