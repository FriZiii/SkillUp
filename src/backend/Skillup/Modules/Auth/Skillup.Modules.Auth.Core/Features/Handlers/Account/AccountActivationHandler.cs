using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Auth.Core.Features.Commands.Account;
using Skillup.Modules.Auth.Core.Repositories;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using Skillup.Shared.Abstractions.Time;

namespace Skillup.Modules.Auth.Core.Features.Handlers.Account
{
    internal class AccountActivationHandler(IUserRepository userRepository, IClock clock, ILogger<AccountActivationHandler> logger) : IRequestHandler<AccountActivationRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IClock _clock = clock;
        private readonly ILogger<AccountActivationHandler> _logger = logger;

        public async Task Handle(AccountActivationRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId) ?? throw new BadRequestException("Account activation failed");

            if (user.TokenExpiration < _clock.CurrentDate())
                throw new BadRequestException("Account activation failed. Invalid activation token");

            if (user.ActivationToken != request.ActivationToken)
                throw new BadRequestException("Account activation failed. Invalid activation token");

            await _userRepository.ChangeState(request.UserId, Entities.UserState.Active);
            _logger.LogInformation("User activated");
        }
    }
}
