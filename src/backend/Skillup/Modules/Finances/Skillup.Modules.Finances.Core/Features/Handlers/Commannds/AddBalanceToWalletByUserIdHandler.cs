using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class AddBalanceToWalletByUserIdHandler : IRequestHandler<AddBalanceToWalletByUserIdRequest>
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ILogger<AddBalanceToWalletByUserIdHandler> _logger;

        public AddBalanceToWalletByUserIdHandler(IWalletRepository walletRepository, ILogger<AddBalanceToWalletByUserIdHandler> logger)
        {
            _walletRepository = walletRepository;
            _logger = logger;
        }

        public async Task Handle(AddBalanceToWalletByUserIdRequest request, CancellationToken cancellationToken)
        {
            await _walletRepository.AddBalanceToWalletByUserId(request.UserId, request.Balance);
            _logger.LogInformation($"{request.Balance} added to balance");
        }
    }
}
