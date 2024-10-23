using Skillup.Shared.Abstractions.Options;
using Skillup.Shared.Infrastructure.EnvironmentInjector;

namespace Skillup.Modules.Mails.Core
{
    public class SmtpOptions : IOption
    {
        [EnvironmentVariable("SMTP_HOST")]
        public string Host { get; set; } = default!;

        [EnvironmentVariable("SMTP_PORT")]
        public int Port { get; set; }

        [EnvironmentVariable("SMTP_SSL_ENABLED")]
        public bool SslEnabled { get; set; }
    }
}
