﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Skillup.Shared.Abstractions.Modules;
using Skillup.Shared.Infrastructure.Api;
using Skillup.Shared.Infrastructure.Modules;
using System.Reflection;

namespace Skillup.Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services,
            IList<Assembly> assemblies, IList<IModule> modules)
        {
            var disabledModules = new List<string>();
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                foreach (var (key, value) in configuration.AsEnumerable())
                {
                    if (!key.Contains(":module:enabled"))
                    {
                        continue;
                    }

                    if (!bool.Parse(value))
                    {
                        disabledModules.Add(key.Split(":")[0]);
                    }
                }
            }

            services.AddSwaggerGen(swagger =>
            {
                swagger.EnableAnnotations();
                swagger.CustomSchemaIds(x => x.FullName);
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Modular API",
                    Version = "v1"
                });

                swagger.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return [api.GroupName];
                    }
                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        return [controllerActionDescriptor.ControllerName];
                    }
                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });

                swagger.DocInclusionPredicate((name, api) => true);
                swagger.DocumentFilter<GroupNameDocumentFilter>();
            });

            services.AddModuleInfo(modules);

            services.AddControllers(options =>
            {
                options.Conventions.Add(new ModulePrefixRouteConventions());

            })
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

            return services;
        }

        public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });
            app.UseCors("cors");
            app.UseRouting();
            app.UseAuthorization();

            return app;
        }
    }
}