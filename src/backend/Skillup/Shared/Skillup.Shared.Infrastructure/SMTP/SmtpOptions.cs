using Skillup.Shared.Abstractions.Options;
using Skillup.Shared.Infrastructure.EnvironmentInjector;

namespace Skillup.Shared.Abstractions
{
    public class SmtpOptions : IOption
    {
        public string SenderEmail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        [EnvironmentVariable("SMTP_HOST")]
        public string Host { get; set; } = default!;

        [EnvironmentVariable("SMTP_PORT")]
        public int Port { get; set; }

        [EnvironmentVariable("SMTP_SSL_ENABLED")]
        public bool SslEnabled { get; set; }
    }
}
