namespace Skillup.Modules.Mails.Core.DTO
{
    internal class SendMailDto
    {
        public string? SenderName { get; set; } = default!;
        public string SenderMail { get; set; } = default!;

        public string? ReciverName { get; set; } = default!;
        public string ReciverMail { get; set; } = default!;

        public string Subject { get; set; } = default!;
        public string Body { get; set; } = default!;
    }
}
