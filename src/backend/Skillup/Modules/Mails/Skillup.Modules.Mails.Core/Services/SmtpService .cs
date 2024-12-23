using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Liquid;
using FluentEmail.Smtp;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Templates;
using Skillup.Shared.Abstractions;
using System.Net;
using System.Net.Mail;

namespace Skillup.Modules.Mails.Core.Services
{
    internal class SmtpService : ISmtpService
    {
        public SmtpService(SmtpOptions options)
        {
            var sender = new SmtpSender(() => new SmtpClient(options.Host)
            {
                EnableSsl = options.SslEnabled,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = options.Port,
                Credentials = new NetworkCredential(options.Username, options.Password)
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
            try
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
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
