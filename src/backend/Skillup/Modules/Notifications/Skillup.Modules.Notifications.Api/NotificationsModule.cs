using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Notifications.Core;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Modules.Smtp.Api
{
    internal class NotificationsModule : IModule
    {
        public string Name => "Notifications";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
