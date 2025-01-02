using MediatR;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Repositories;
using Skillup.Modules.Mails.Core.Services;
using Skillup.Modules.Mails.Core.Templates.PasswordChange;
using Skillup.Shared.Abstractions;
using Skillup.Shared.Abstractions.Exceptions.GlobalExceptions;

namespace Skillup.Modules.Mails.Core.Commands.Handlers
{
    internal class PasswordChangedHandler(IUserRepository userRepository,
        ISmtpService smtpService,
        SmtpOptions smtpOptions,
        ILogger<AccountActivationHandler> logger
        ) : IRequestHandler<PasswordChangedRequest>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ISmtpService _smtpService = smtpService;
        private readonly SmtpOptions _smtpOptions = smtpOptions;
        private readonly ILogger<AccountActivationHandler> _logger = logger;

        public async Task Handle(PasswordChangedRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId) ?? throw new NotFoundException($"User with ID {request.UserId} not found");

            var sender = new Participant() { Email = _smtpOptions.SenderEmail, Name = "SkillUp" };
            var reciver = new Participant() { Email = user.Email };
            var template = new PasswordChangedTemplate(_smtpOptions.SenderEmail);

            await _smtpService.SendEmail(sender, reciver, template);
            _logger.LogInformation("Password changed");
        }
    }
}
