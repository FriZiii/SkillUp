using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class CreateUserWalletHandler : IRequestHandler<CreateUserWalletRequest>
    {
        private readonly IWalletRepository _walletRepository;

        public CreateUserWalletHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task Handle(CreateUserWalletRequest request, CancellationToken cancellationToken)
        {
            await _walletRepository.CreateUserWallet(new Wallet(request.UserId));
        }
    }
}
