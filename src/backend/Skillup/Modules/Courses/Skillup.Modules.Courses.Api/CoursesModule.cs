using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Modules.Courses.Application;
using Skillup.Modules.Courses.Core;
using Skillup.Modules.Courses.Infrastracture;
using Skillup.Shared.Abstractions.Modules;

namespace Skillup.Modules.Courses.Api
{
    internal class CoursesModule : IModule
    {
        public string Name => "Courses";

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
