using MediatR;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Skillup.Modules.Finances.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class WalletsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [SwaggerOperation("Get user wallet by user id")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWallet(Guid userId)
        {
            var wallet = await _mediator.Send(new GetUserWalletByOwnerIdRequest(userId));
            return Ok(wallet);
        }

        [SwaggerOperation("Add balance to user wallet")]
        [HttpPut("{walletId}")]
        public async Task<IActionResult> AddBalanceToWallet(Guid walletId, int balance)
        {
            await _mediator.Send(new AddBalanceToWalletRequest(walletId, balance));

            var wallet = await _mediator.Send(new GetUserWalletByIdRequest(walletId));
            return Ok(wallet);
        }
    }
}
