using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetUserWalletByIdHandler(IWalletRepository walletRepository) : IRequestHandler<GetUserWalletByIdRequest, WalletDto>
    {
        private readonly IWalletRepository _walletRepository = walletRepository;

        public async Task<WalletDto> Handle(GetUserWalletByIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new WalletMapper();
            var wallet = await _walletRepository.GetWallet(request.WalletId) ?? throw new Exception(); // TODO: Custom Ex;
            return mapper.WalletToDto(wallet);
        }
    }
}
