using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Finances.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class CartController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{cartId}")]
        [SwaggerOperation("Get cart")]
        public async Task<IActionResult> GetCart(Guid cartId)
        {
            return Ok(await _mediator.Send(new GetCartByIdRequest(cartId)));
        }

        [HttpPost("Items")]
        [SwaggerOperation("Add item to cart")]
        public async Task<IActionResult> AddCartItem(Guid? cartId, Guid itemId)
        {
            var request = new AddCartItemRequest(cartId, itemId);
            await _mediator.Send(request);
            return Ok(await _mediator.Send(new GetCartByIdRequest((Guid)request.CartId!)));
        }

        [HttpDelete("{cartId}/Items/{itemId}")]
        [SwaggerOperation("Delete cart item")]
        public async Task<IActionResult> DeleteCartItem(Guid cartId, Guid itemId)
        {
            await _mediator.Send(new DeleteItemFromCartRequest(cartId, itemId));
            return Ok();
        }

        [HttpPost("{cartId}/discount-code")]
        [SwaggerOperation("Toggle discount code for cart")]
        public async Task<IActionResult> ToggleDiscountCodeForCart(Guid cartId, [FromQuery] string? discountCode)
        {
            await _mediator.Send(new ToggleDiscountCodeForCartRequest(cartId, discountCode));
            return Ok(await _mediator.Send(new GetCartByIdRequest(cartId)));
        }
    }
}
