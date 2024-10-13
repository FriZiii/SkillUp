using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests;
using Skillup.Shared.Abstractions.Auth;
using Skillup.Shared.Infrastructure.Auth;

namespace Skillup.Modules.Finances.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;

        public ItemsController(IMediator mediator, IPublishEndpoint publishEndpoint)
        {
            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{itemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetItemById(Guid itemId)
        {
            return Ok(await _mediator.Send(new GetItemByIdRequest(itemId)));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetItemById([FromQuery] ItemType itemType)
        {
            return Ok(await _mediator.Send(new GetItemsByTypeRequest(itemType)));
        }

        [HttpPut("{itemId}")]
        [Authorize(Roles = nameof(UserRole.CourseAuthor))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditItemPrice(Guid itemId, EditItemPriceRequest request)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            request.UserId = (Guid)userId;
            request.ItemId = itemId;
            await _mediator.Send(request);

            var editedItem = _mediator.Send(new GetItemByIdRequest(itemId));
            return Ok(editedItem);
        }
    }
}
