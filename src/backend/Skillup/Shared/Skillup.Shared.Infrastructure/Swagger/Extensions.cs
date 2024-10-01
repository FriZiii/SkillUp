using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Skillup.Shared.Infrastructure.Swagger
{
    internal static class Extensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                ConfigureSwaggerBasics(swagger);
                ConfigureSwaggerTags(swagger);
                ConfigureSwaggerSecurity(swagger);
            });

            return services;
        }

        private static void ConfigureSwaggerBasics(SwaggerGenOptions swagger)
        {
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Modular API",
                Version = "v1"
            });
        }

        private static void ConfigureSwaggerTags(SwaggerGenOptions swagger)
        {
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
        }

        private static void ConfigureSwaggerSecurity(SwaggerGenOptions swagger)
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            swagger.AddSecurityRequirement(
                new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        }
    }
}
