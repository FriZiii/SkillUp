using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class AddBalanceToWalletHandler : IRequestHandler<AddBalanceToWalletRequest>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<AddBalanceToWalletHandler> _logger;

        public AddBalanceToWalletHandler(IWalletRepository walletRepository, ILogger<AddBalanceToWalletHandler> logger)
        {
            _walletRepository = walletRepository;
            _logger = logger;
        }

        public async Task Handle(AddBalanceToWalletRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWallet(request.WalletId) ?? throw new Exception(); // TODO: Custom ex: wallet with id doesnt exist
            wallet.AddToBalance(request.Balance);
            var history = new BalanceHistory(wallet.Id, wallet.Balance, "Money transfer", "Add");
            await _walletRepository.UpdateWalletBalance(wallet, history);
            _logger.LogInformation($"{request.Balance} added to balance");
        }
    }
}
