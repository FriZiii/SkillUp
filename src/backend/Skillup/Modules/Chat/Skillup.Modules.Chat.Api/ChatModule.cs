using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Chat.Core;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Modules.Chat.Api
{
    internal class ChatModule : IModule
    {
        public string Name => "Chat";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {

        }
    }
}
