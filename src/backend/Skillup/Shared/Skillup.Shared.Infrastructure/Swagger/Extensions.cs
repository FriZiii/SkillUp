using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Skillup.Modules.Finances.Core.Entities;
using Skillup.Shared.Abstractions.Auth;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Skillup.Shared.Infrastructure.Swagger
{
    internal static class Extensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var swaggerOptions = services.GetOptions<SwaggerOptions>("Swagger");

            services.AddSingleton(swaggerOptions);
            services.AddSwaggerGen(swagger =>
            {
                ConfigureSwaggerBasics(swagger, swaggerOptions);
                ConfigureEnumMapping(swagger);
                ConfigureSwaggerTags(swagger);
                ConfigureSwaggerSecurity(swagger);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerWithUi(this IApplicationBuilder app)
        {
            if (((WebApplication)app).Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }

        private static void ConfigureSwaggerBasics(SwaggerGenOptions swagger, SwaggerOptions swaggerOptions)
        {
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc(swaggerOptions.Version, new OpenApiInfo
            {
                Title = swaggerOptions.Title,
                Version = swaggerOptions.Version
            });
        }

        private static void ConfigureEnumMapping(SwaggerGenOptions swagger)
        {
            swagger.MapType<ItemType>(() => new OpenApiSchema
            {
                Type = "string",
                Enum = Enum.GetNames(typeof(ItemType)).Select(name => (IOpenApiAny)new OpenApiString(name)).ToList()
            });

            swagger.MapType<UserRole>(() => new OpenApiSchema
            {
                Type = "string",
                Enum = Enum.GetNames(typeof(UserRole)).Select(name => (IOpenApiAny)new OpenApiString(name)).ToList()
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
