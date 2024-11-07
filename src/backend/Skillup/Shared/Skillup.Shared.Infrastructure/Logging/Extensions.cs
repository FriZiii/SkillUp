using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace Skillup.Shared.Infrastructure.Logging
{
    public static class Extensions
    {
        public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            return builder;
        }
    }

}
