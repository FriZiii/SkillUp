using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Services;
using Skillup.Modules.Mails.Core.Templates.PasswordReset;
using Skillup.Shared.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
namespace Skillup.Modules.Mails.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class MailController(ISmtpService smtpService, SmtpOptions smtpOptions) : Controller
    {
        private readonly ISmtpService _smtpService = smtpService;
        private readonly SmtpOptions _smtpOptions = smtpOptions;

        [HttpGet("Test")]
        [SwaggerOperation("Test email")]
        public async Task<IActionResult> SendTestEmail()
        {
            var sender = new Participant() { Email = _smtpOptions.SenderEmail, Name = "SkillUp" };
            var template = new PasswordResetRequestedTemplate("123123", "123123");

            await _smtpService.SendEmail(sender, sender, template);
            return Ok();
        }
    }
}
