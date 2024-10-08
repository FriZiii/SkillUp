using Microsoft.AspNetCore.Builder;
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
            var swaggerOptions = services.GetOptions<SwaggerOptions>("Swagger");

            if (!swaggerOptions.Enabled)
                return services;

            services.AddSingleton(swaggerOptions);
            services.AddSwaggerGen(swagger =>
            {
                ConfigureSwaggerBasics(swagger, swaggerOptions);
                ConfigureSwaggerTags(swagger);
                ConfigureSwaggerSecurity(swagger);
            });

            return services;
        }

        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            //var swaggerOptions = app.ApplicationServices.GetService<SwaggerOptions>();
            //if (swaggerOptions != null && swaggerOptions.Enabled)
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

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
