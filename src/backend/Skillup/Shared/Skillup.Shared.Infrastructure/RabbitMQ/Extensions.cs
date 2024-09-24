using MassTransit;
using MassTransit.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skillup.Shared.Infrastructure.EnvironmentInjector;

namespace Skillup.Shared.Infrastructure.RabbitMQ
{
    public static class Extensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services)
        {
            RabbitMqOptions options = (RabbitMqOptions)new RabbitMqOptions().InjectEnvironment();
            services.AddSingleton(options);

            services.AddMassTransit(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();

                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{options.Host}"), h =>
                    {
                        h.Username(options.User);
                        h.Password(options.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }

        public static IServiceCollection AddConsumer<T>(this IServiceCollection services) where T : class, IConsumer
        {
            var registrar = new DependencyInjectionContainerRegistrar(services);
            services.RegisterConsumer<T>(registrar);

            return services;
        }
    }
}
