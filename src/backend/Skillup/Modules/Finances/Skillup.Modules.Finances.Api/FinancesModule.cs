using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Finances.Core;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Modules.Finances.Api
{
    internal class FinancesModule : IModule
    {
        public string Name => "FinancesModule";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {

        }
    }
}
