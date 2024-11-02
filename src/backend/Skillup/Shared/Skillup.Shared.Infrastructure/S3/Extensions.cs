using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skillup.Shared.Abstractions.S3;
using Skillup.Shared.Infrastructure.EnvironmentInjector;

namespace Skillup.Shared.Infrastructure.S3
{
    public static class Extensions
    {
        public static IServiceCollection AddAwsS3(this IServiceCollection services)
        {
            var options = (AmazonS3Options)services.GetOptions<AmazonS3Options>("AmazonS3").InjectEnvironment();
            services.AddSingleton(options);

            services.AddSingleton<IAmazonS3>(provider =>
            {
                var env = provider.GetRequiredService<IWebHostEnvironment>();
                var cred = new BasicAWSCredentials(options.AccessKey, options.SecretKey);

                AmazonS3Config config;
                if (env.IsDevelopment())
                {
                    config = new AmazonS3Config
                    {
                        ServiceURL = $"{options.ServiceURL}:{options.Port}",
                        ForcePathStyle = true,
                    };
                }
                else
                {
                    config = new AmazonS3Config
                    {
                        RegionEndpoint = RegionEndpoint.GetBySystemName(options.Region),
                    };
                }

                return new AmazonS3Client(cred, config);
            });

            services.AddScoped<IAmazonS3Service, AmazonS3Service>();

            return services;
        }
    }
}
