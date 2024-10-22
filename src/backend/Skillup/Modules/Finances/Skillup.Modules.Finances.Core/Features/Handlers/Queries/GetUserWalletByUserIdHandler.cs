using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Queries;
using Skillup.Modules.Finances.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Queries
{
    internal class GetUserWalletByUserIdHandler : IRequestHandler<GetUserWalletByUserIdRequest, Wallet>
    {
        private readonly IWalletRepository _walletRepository;

        public GetUserWalletByUserIdHandler(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        public async Task<Wallet> Handle(GetUserWalletByUserIdRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _walletRepository.GetWalletByUserId(request.UserId) ?? throw new UserNotFoundException(request.UserId);
            return wallet;
        }
    }
}
