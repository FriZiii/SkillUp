using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Mails.Core;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Modules.Mails.Api
{
    internal class MailsModule : IModule
    {
        public string Name => "Mails";

        public void Register(IServiceCollection services)
        {
            services.AddCore();
        }

        public void Use(IApplicationBuilder app)
        {
        }
    }
}
