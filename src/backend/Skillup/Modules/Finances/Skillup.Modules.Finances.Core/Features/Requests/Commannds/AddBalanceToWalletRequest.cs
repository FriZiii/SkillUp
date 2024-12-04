using MediatR;

namespace Skillup.Modules.Finances.Core.Features.Requests.Commannds
{
    internal record AddBalanceToWalletRequest(Guid WalletId, decimal Balance) : IRequest;
}
