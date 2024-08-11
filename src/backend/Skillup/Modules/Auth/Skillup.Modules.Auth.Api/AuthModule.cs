using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Auth.Application;
using Skillup.Modules.Auth.Core;
using Skillup.Modules.Auth.Infrastructure;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Modules.Auth.Api
{
    internal class AuthModule : IModule
    {
        public string Name => "Auth";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
            services.AddApplication();
            services.AddInfrastructure();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
