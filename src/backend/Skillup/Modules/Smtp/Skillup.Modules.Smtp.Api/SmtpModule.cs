using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Smtp.Core;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Modules.Smtp.Api
{
    internal class SmtpModule : IModule
    {
        public string Name => "Smtp";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
