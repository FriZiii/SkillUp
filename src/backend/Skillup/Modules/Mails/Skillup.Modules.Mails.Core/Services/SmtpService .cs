using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Liquid;
using FluentEmail.Smtp;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Templates;
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
        {
            return await Email
                .From(sender.Email, sender.Name)
                .To(recviver.Email, recviver.Name)
                .Subject(message.Subject)
                .Body(message.Body)
                .SendAsync();
        }

        public async Task<SendResponse> SendEmail(Participant sender, Participant recviver, ITemplate template)
        {
            var templatesFolderPath = Path.Combine(AppContext.BaseDirectory, "Templates");

            var fileProvider = new PhysicalFileProvider(templatesFolderPath);
            var options = new LiquidRendererOptions
            {
                FileProvider = fileProvider
            };

            Email.DefaultRenderer = new LiquidRenderer(Options.Create(options));

            return await Email
                .From(sender.Email, sender.Name)
                .To(recviver.Email, recviver.Name)
                .Subject(template.Subject)
                .UsingTemplateFromFile(Path.Combine(templatesFolderPath, template.Path), template.Model)
                .SendAsync();
        }
    }
}
