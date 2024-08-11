using Skillup.Shared.Abstractions.Options;
using Skillup.Shared.Infrastructure.EnvironmentInjector;

namespace Skillup.Shared.Infrastructure.Postgres
{
    public class PostgresOptions(string connectionStringTemplate) : IOption
    {
        [EnvironmentVariable("POSTGRES_HOST")]
        public string Host { get; set; } = default!;

        [EnvironmentVariable("POSTGRES_DB")]
        public string DatabaseName { get; set; } = default!;

        [EnvironmentVariable("POSTGRES_USER")]
        public string User { get; set; } = default!;

        [EnvironmentVariable("POSTGRES_PASSWORD")]
        public string Password { get; set; } = default!;

        [EnvironmentVariable("POSTGRES_PORT")]
        public int Port { get; set; } = default!;

        public string ConnectionString { get => connectionStringTemplate; }
    }
}
