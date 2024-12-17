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

        [Authorize]
        [SwaggerOperation("Add balance to user wallet")]
        [HttpPut("{walletId}")]
        public async Task<IActionResult> AddBalanceToWallet(Guid walletId, int balance)
        {
            var userId = User.GetUserId();
            if (userId == null) return Unauthorized();

            await _mediator.Send(new AddBalanceToWalletRequest(walletId, balance));

            var wallet = await _mediator.Send(new GetUserWalletByOwnerIdRequest((Guid)userId));
            return Ok(wallet);
        }
    }
}
