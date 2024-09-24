using Skillup.Shared.Abstractions.Options;
using Skillup.Shared.Infrastructure.EnvironmentInjector;

namespace Skillup.Shared.Infrastructure.RabbitMQ
{
    internal class RabbitMqOptions : IOption
    {
        [EnvironmentVariable("RABBITMQ_HOST")]
        public string Host { get; set; } = default!;

        [EnvironmentVariable("RABBITMQ_DEFAULT_USER")]
        public string User { get; set; }

        [EnvironmentVariable("RABBITMQ_DEFAULT_PASS")]
        public string Password { get; set; }
    }
}
