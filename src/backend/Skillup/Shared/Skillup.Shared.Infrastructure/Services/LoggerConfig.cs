using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Shared.Infrastructure.Services
{
    public static class LoggerConfig
    {
        public static Serilog.ILogger CreateLogger(WebApplicationBuilder builder, List<IModule> modules)
        {
            var loggerConfig = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console();

            //Loging to Files
            //foreach (var module in modules)
            //{
            //    loggerConfig = loggerConfig.WriteTo.Logger(lc => lc
            //        .Filter.ByIncludingOnly(Matching.FromSource($"Skillup.Modules.{module.Name}"))
            //        .WriteTo.File($"logs/{module.Name}.txt")
            //    );
            //}
            Log.Logger = loggerConfig.CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog();

            return Log.Logger;
        }
    }

}
