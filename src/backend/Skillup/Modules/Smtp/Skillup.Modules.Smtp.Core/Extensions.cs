using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("Skillup.Modules.Smtp.Api")]
namespace Skillup.Modules.Smtp.Core;
internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services;
    }
}