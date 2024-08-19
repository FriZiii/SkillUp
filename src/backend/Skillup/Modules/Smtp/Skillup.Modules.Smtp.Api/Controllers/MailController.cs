﻿using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skillup.Modules.Mails.Core;
using Skillup.Modules.Mails.Core.DTO;
using System.Net.Mail;
namespace Skillup.Modules.Mails.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class MailController(SmtpOptions smtpOptions, ILogger<MailController> logger) : Controller
    {
        private readonly SmtpOptions _smtpOptions = smtpOptions;
        private readonly ILogger<MailController> _logger = logger;

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendMailDto sendMailDto)
        {
            _logger.LogInformation("Wysyłania maila");
            _logger.LogDebug("Loguje Debug");

            var sender = new SmtpSender(() => new SmtpClient(_smtpOptions.Host)
            {
                EnableSsl = _smtpOptions.SslEnabled,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = _smtpOptions.Port,
            });

            Email.DefaultSender = sender;

            var email = await Email
                .From(sendMailDto.SenderMail, sendMailDto.SenderName)
                .To(sendMailDto.ReciverMail, sendMailDto.ReciverName)
                .Subject(sendMailDto.Subject)
                .Body(sendMailDto.Body)
                .SendAsync();

            return Ok(email);
        }
    }
}
