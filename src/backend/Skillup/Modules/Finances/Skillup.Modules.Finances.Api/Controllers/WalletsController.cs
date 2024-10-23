using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Finances.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class WalletsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation("Get user wallet by user id")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWallet(Guid userId)
        {
            var wallet = await _mediator.Send(new GetUserWalletByUserIdRequest(userId));
            return Ok(wallet);
        }

        [SwaggerOperation("Add balance to user wallet")]
        [HttpPut("{userId}")]
        public async Task<IActionResult> AddBalanceToUserWallet(Guid userId, decimal balance)
        {
            await _mediator.Send(new AddBalanceToWalletByUserIdRequest(userId, balance));

            var wallet = await _mediator.Send(new GetUserWalletByUserIdRequest(userId));
            return Ok(wallet);
        }
    }
}
