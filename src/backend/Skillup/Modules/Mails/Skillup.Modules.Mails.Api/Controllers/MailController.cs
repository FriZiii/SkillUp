using Microsoft.AspNetCore.Mvc;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Services;
using Swashbuckle.AspNetCore.Annotations;
namespace Skillup.Modules.Mails.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    internal class MailController(ISmtpService smtpService) : Controller
    {
        private readonly ISmtpService _smtpService = smtpService;

        [HttpPost]
        [SwaggerOperation("Send email")]
        public async Task<IActionResult> SendEmail(SendMailDto mailDto)
        {
            var result = await _smtpService.SendEmail(mailDto.Sender, mailDto.Reciver, mailDto.Message);
            return Ok(result);
        }
    }
}
