using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Repositories;
using Skillup.Modules.Mails.Core.Services;
using Skillup.Modules.Mails.Core.Templates.PasswordReset;
using Skillup.Shared.Abstractions;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;
using Skillup.Shared.Infrastructure.Client;

namespace Skillup.Modules.Mails.Core.Commands.Handlers
{
    internal class PasswordResetHandler(IUserRepository userRepository, ISmtpService smtpService,
                                        ClientOptions clientOptions, SmtpOptions smtpOptions,
                                        ILogger<AccountActivationHandler> logger) : IRequestHandler<PasswordResetRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ISmtpService _smtpService = smtpService;
        private readonly ClientOptions _clientOptions = clientOptions;
        private readonly SmtpOptions _smtpOptions = smtpOptions;
        private readonly ILogger<AccountActivationHandler> _logger = logger;

        public async Task Handle(PasswordResetRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId) ?? throw new NotFoundException($"User with ID {request.UserId} not found");

            var sender = new Participant() { Email = _smtpOptions.SenderEmail, Name = "SkillUp" };
            var reciver = new Participant() { Email = user.Email };
            var template = new PasswordResetRequestedTemplate(request.Token, _clientOptions.ClientUrl);

            await _smtpService.SendEmail(sender, reciver, template);
            _logger.LogInformation("Reset password requested email sent");
        }
    }
}
