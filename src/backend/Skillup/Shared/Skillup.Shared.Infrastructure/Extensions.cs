﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Shared.Abstractions.Modules;
using Skillup.Shared.Abstractions.Storage;
using Skillup.Shared.Abstractions.Time;
using Skillup.Shared.Infrastructure.Api;
using Skillup.Shared.Infrastructure.Auth;
using Skillup.Shared.Infrastructure.Client;
using Skillup.Shared.Infrastructure.Exceptions;
using Skillup.Shared.Infrastructure.Modules;
using Skillup.Shared.Infrastructure.Postgres;
using Skillup.Shared.Infrastructure.RabbitMQ;
using Skillup.Shared.Infrastructure.S3;
using Skillup.Shared.Infrastructure.Seeder;
using Skillup.Shared.Infrastructure.Services;
using Skillup.Shared.Infrastructure.SMTP;
using Skillup.Shared.Infrastructure.Storage;
using Skillup.Shared.Infrastructure.Swagger;
using Skillup.Shared.Infrastructure.Time;
using System.Text.Json.Serialization;
using SkillupCorsExtensions = Skillup.Shared.Infrastructure.Cors.Extensions;


namespace Skillup.Shared.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddModularInfrastructure(this IServiceCollection services, IList<IModule> modules)
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

                    if (value != null && bool.TryParse(value, out var result) && !result)
                    {
                        disabledModules.Add(key.Split(":")[0]);
                    }
                }
            }

            services.AddSwagger();

            services.AddModuleInfo(modules);

            services.AddSignalR();
            services.AddPostgres();
            services.AddRabbitMQ();
            services.AddAwsS3();
            services.AddMemoryCache();
            services.AddAuth();
            services.AddClient();
            services.AddSmpt();
            SkillupCorsExtensions.AddCors(services);

            services.AddSingleton<IMemoryStorage, MemoryStorage>();
            services.AddSingleton<IClock, UtcClock>();

            services.AddErrorHandling();

            services.AddHostedService<DatabaseMigrationService>();
            services.AddHostedService<DatabaseSeederService>();

            services.AddControllers(options =>
            {
                options.Conventions.Add(new ControllerModelConvention());

            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            SkillupCorsExtensions.UseCors(app);

            app.UseErrorHandling();
            app.UseAuth();
            app.UseAuthorization();
            app.UseSwaggerWithUi();

            return app;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }

        public static IConfigurationSection GetSection(this IServiceCollection services, string sectionName)
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetSection(sectionName);
        }
    }
}
