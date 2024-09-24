using FluentEmail.Core.Models;
using Skillup.Modules.Mails.Core.DTO;

namespace Skillup.Modules.Mails.Core.Services
{
    internal interface ISmtpService
    {
        Task<SendResponse> SendEmail(Participant sender, Participant recviver, Message message);
    }
}