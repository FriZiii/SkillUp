using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record AddBalanceToWalletByUserIdRequest(Guid UserId, decimal Balance) : IRequest;
}
