using MediatR;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class AddBalanceToWalletByUserIdHandler : IRequestHandler<AddBalanceToWalletByUserIdRequest>
    {
        private readonly IWalletRepository _walletRepository;

        public AddBalanceToWalletByUserIdHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task Handle(AddBalanceToWalletByUserIdRequest request, CancellationToken cancellationToken)
        {
            await _walletRepository.AddBalanceToWalletByUserId(request.UserId, request.Balance);
        }
    }
}
