using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Shared.Infrastructure.Modules
{
    public static class Extensions
    {
        public static IServiceCollection AddModuleInfo(this IServiceCollection services, IList<IModule> modules)
        {
            var moduleInfoProvider = new ModuleInfoProvider();
            var moduleInfo =
                modules?.Select(x => new ModuleInfo(x.Name, x.Policies ?? Enumerable.Empty<string>())) ??
                Enumerable.Empty<ModuleInfo>();
            moduleInfoProvider.Modules.AddRange(moduleInfo);
            services.AddSingleton(moduleInfoProvider);

            return services;
        }

        public static void MapModuleInfo(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet("modules", context =>
            {
                var moduleInfoProvider = context.RequestServices.GetRequiredService<ModuleInfoProvider>();
                return context.Response.WriteAsJsonAsync(moduleInfoProvider.Modules);
            });
        }

        public static IHostApplicationBuilder ConfigureModules(this IHostApplicationBuilder builder)
        {
            foreach (var settings in GetSettings(builder.Environment.ContentRootPath, "*"))
            {
                builder.Configuration.AddJsonFile(settings, optional: true, reloadOnChange: true);
            }
            return builder;
        }

        private static IEnumerable<string> GetSettings(string rootPath, string pattern)
            => Directory.EnumerateFiles(rootPath, $"module.{pattern}.json", SearchOption.AllDirectories);
    }
}
