using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Smtp;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Shared.Infrastructure.EnvironmentInjector;
using System.Net.Mail;

namespace Skillup.Modules.Mails.Core.Services
{
    internal class SmtpService : ISmtpService
    {
        private readonly SmtpOptions _smtpOptions;

        public SmtpService()
        {
            _smtpOptions = (SmtpOptions)new SmtpOptions().InjectEnvironment();

            var sender = new SmtpSender(() => new SmtpClient(_smtpOptions.Host)
            {
                EnableSsl = _smtpOptions.SslEnabled,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = _smtpOptions.Port,
            });

            Email.DefaultSender = sender;
        }

        public async Task<SendResponse> SendEmail(Participant sender, Participant recviver, Message message)
         => await Email
                .From(sender.Email, sender.Name)
                .To(recviver.Email, recviver.Name)
                .Subject(message.Subject)
                .Body(message.Body)
                .SendAsync();
    }
}
