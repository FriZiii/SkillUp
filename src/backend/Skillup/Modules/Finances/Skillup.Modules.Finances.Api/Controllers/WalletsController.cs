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
        internal enum AllowedBalance
        {
            Fifty = 50,
            SeventyFive = 75,
            OneHundred = 100,
            TwoHundred = 200,
            ThreeHundred = 300,
            FiveHundred = 500
        }

        [SwaggerOperation("Get user wallet by user id")]
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserWallet(Guid userId)
        {
            var wallet = await _mediator.Send(new GetUserWalletByOwnerIdRequest(userId));
            return Ok(wallet);
        }

        [SwaggerOperation("Add balance to user wallet")]
        [HttpPut("{walletId}")]
        public async Task<IActionResult> AddBalanceToWallet(Guid walletId, [FromQuery] AllowedBalance balance)
        {
            await _mediator.Send(new AddBalanceToWalletRequest(walletId, (int)balance));

            var wallet = await _mediator.Send(new GetUserWalletByIdRequest(walletId));
            return Ok(wallet);
        }
    }
}
