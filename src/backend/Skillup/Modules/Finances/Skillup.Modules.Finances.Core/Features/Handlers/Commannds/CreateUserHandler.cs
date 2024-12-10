using MediatR;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Modules.Finances.Core.Features.Requests.Commannds;
using Skillup.Modules.Finances.Core.Repositories;

namespace Skillup.Modules.Finances.Core.Features.Handlers.Commannds
{
    internal class CreateUserHandler(IWalletRepository walletRepository, IUserRepository userRepository) : IRequestHandler<CreateUserRequest>
    {
        private readonly IWalletRepository _walletRepository = walletRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Id = request.UserId,
            };

            await _userRepository.Add(user);
            await _walletRepository.Add(new Wallet(user));
        }
    }
}
