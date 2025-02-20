﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Finances.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class OrderController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        [HttpGet("{ordererId}")]
        [SwaggerOperation("Get orders by orderer id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrdersByOrderer(Guid ordererId)
        {
            return Ok(await _mediator.Send(new GetOrdersByOrdererIdRequest(ordererId)));
        }

        [Authorize]
        [HttpGet]
        [SwaggerOperation("Get order by balance history id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetOrderByBalanceHistoryId([FromQuery] Guid balanceHistoryId)
        {
            return Ok(await _mediator.Send(new GetOrdersByBalanceHistoryIdRequest(balanceHistoryId)));
        }
    }
}
