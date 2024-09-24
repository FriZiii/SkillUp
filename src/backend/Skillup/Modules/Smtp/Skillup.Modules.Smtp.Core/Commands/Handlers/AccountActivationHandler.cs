﻿using MediatR;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Services;

namespace Skillup.Modules.Mails.Core.Commands.Handlers
{
    internal class AccountActivationHandler : IRequestHandler<AccountActivation>
    {
        private readonly ISmtpService _smtpService;

        public AccountActivationHandler(ISmtpService smtpService)
        {
            _smtpService = smtpService;
        }

        public async Task Handle(AccountActivation request, CancellationToken cancellationToken)
        {
            var sender = new Participant() { Email = "skillup@noreplay.com", Name = "SkillUp" };
            var reciver = new Participant() { Email = request.Email };
            var message = new Message() { Subject = "Test", Body = "LoremIpsum" };

            await _smtpService.SendEmail(sender, reciver, message);
        }
    }
}
