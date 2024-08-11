using FluentEmail.Core;
using FluentEmail.Smtp;
using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Mails.Core;
using Skillup.Modules.Mails.Core.DTO;
using System.Net.Mail;
namespace Skillup.Modules.Mails.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class MailController : Controller
    {
        private readonly SmtpOptions _smtpOptions;
        public MailController(SmtpOptions smtpOptions)
        {
            _smtpOptions = smtpOptions;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendMailDto sendMailDto)
        {
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
