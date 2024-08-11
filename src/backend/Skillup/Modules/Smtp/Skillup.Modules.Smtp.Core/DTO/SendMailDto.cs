namespace Skillup.Modules.Mails.Core.DTO
{
    internal class SendMailDto
    {
        public string? SenderName { get; set; }
        public string SenderMail { get; set; }

        public string? ReciverName { get; set; }
        public string ReciverMail { get; set; }

        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
