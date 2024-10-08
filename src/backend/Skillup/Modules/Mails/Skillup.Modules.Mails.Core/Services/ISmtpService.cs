using FluentEmail.Core.Models;
using Skillup.Modules.Mails.Core.DTO;
using Skillup.Modules.Mails.Core.Templates;

namespace Skillup.Modules.Mails.Core.Services
{
    internal interface ISmtpService
    {
        Task<SendResponse> SendEmail(Participant sender, Participant recviver, Message message);
        Task<SendResponse> SendEmail(Participant sender, Participant recviver, ITemplate template);
    }
}