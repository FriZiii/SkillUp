namespace Skillup.Modules.Mails.Core.DTO
{
    internal class SendMailDto
    {
        public Participant Sender { get; set; } = default!;
        public Participant Reciver { get; set; } = default!;
        public Message Message { get; set; } = default!;
    }
}
