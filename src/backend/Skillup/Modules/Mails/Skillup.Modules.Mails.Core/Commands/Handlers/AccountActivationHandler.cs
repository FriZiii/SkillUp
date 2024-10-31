using MediatR;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Services;
using Skillup.Modules.Mails.Core.Templates.AccountActivation;
using Skillup.Shared.Abstractions;
using Skillup.Shared.Infrastructure.Client;

namespace Skillup.Modules.Mails.Core.Commands.Handlers
{
    internal class AccountActivationHandler : IRequestHandler<AccountActivation>
    {
        private readonly ISmtpService _smtpService;
        private readonly ClientOptions _clientOptions;
        private readonly SmtpOptions _smtpOptions;

        public AccountActivationHandler(ISmtpService smtpService, ClientOptions clientOptions, SmtpOptions smtpOptions)
        {
            _smtpService = smtpService;
            _clientOptions = clientOptions;
            _smtpOptions = smtpOptions;
        }

        public async Task Handle(AccountActivation request, CancellationToken cancellationToken)
        {
            var sender = new Participant() { Email = _smtpOptions.SenderEmail, Name = "SkillUp" };
            var reciver = new Participant() { Email = request.Email };
            var template = new AccountActivationTemplate(request.UserId, request.ActivationToken, request.TokenExpiration, _clientOptions.ClientUrl);

            await _smtpService.SendEmail(sender, reciver, template);
        }
    }
}
