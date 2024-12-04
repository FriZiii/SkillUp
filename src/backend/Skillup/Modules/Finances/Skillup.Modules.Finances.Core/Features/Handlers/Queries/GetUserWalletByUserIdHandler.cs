using MediatR;
using Skillup.Modules.Finances.Core.DTO;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Mappings;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetUserWalletByUserIdHandler(IWalletRepository walletRepository) : IRequestHandler<GetUserWalletByOwnerIdRequest, WalletDto>
    {
        private readonly IWalletRepository _walletRepository = walletRepository;

        public async Task<WalletDto> Handle(GetUserWalletByOwnerIdRequest request, CancellationToken cancellationToken)
        {
            var mapper = new WalletMapper();
            var wallet = await _walletRepository.GetWalletByOwnerId(request.UserId) ?? throw new UserNotFoundException(request.UserId);
            return mapper.WalletToDto(wallet);
        }
    }
}
