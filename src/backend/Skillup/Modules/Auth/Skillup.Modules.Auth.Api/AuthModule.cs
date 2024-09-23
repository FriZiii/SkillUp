using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Auth.Core;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Modules.Auth.Api
{
    internal class AuthModule : IModule
    {
        public string Name => "Auth";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
