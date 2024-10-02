using MediatR;
using Skillup.Modules.Auth.Core.Exceptions;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Auth.Core.Commands.Handlers
{
    internal class AccountActivationHandler(IUserRepository userRepository, IClock clock) : IRequestHandler<AccountActivation>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IClock _clock = clock;

        public async Task Handle(AccountActivation request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId) ?? throw new UserNotFoundException(request.UserId);

            if (user.TokenExpiration < _clock.CurrentDate())
                throw new ActivationCodeExpiredException();

            if (user.ActivationToken != request.ActivationToken)
                throw new InvalidActivationCodeException();

            await _userRepository.ChangeState(request.UserId, Entities.UserState.Active);
        }
    }
}
