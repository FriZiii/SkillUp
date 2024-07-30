using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Modules.Smtp.Api
{
    internal class NotificationsModule : IModule
    {
        public string Name => "Notifications";

        public void Register(IServiceCollection services)
        {

        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
