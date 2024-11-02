using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Skillup.Shared.Infrastructure.SerilogLogger
{
    public static class LoggerConfig
    {
        public static Serilog.ILogger CreateLogger(WebApplicationBuilder builder)
        {
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console();

            Log.Logger = loggerConfig.CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog();

            return Log.Logger;
        }
    }

}
