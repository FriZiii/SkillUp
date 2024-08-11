using Skillup.Shared.Abstractions.Options;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Skillup.Shared.Infrastructure.EnvironmentInjector
{
    public static partial class Extensions
    {
        [GeneratedRegex(@"\{([^}]*)\}")]
        private static partial Regex EnvironmentVariableRegex();

        public static string InjectEnvironment(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return EnvironmentVariableRegex().Replace(value, match =>
            {
                var variableName = match.Groups[1].Value;
                var environmentValue = Environment.GetEnvironmentVariable(variableName);
                return environmentValue ?? match.Value;
            });
        }

        public static IOption InjectEnvironment(this IOption option)
        {
            var properties = option.GetType()
                .GetProperties()
                .Where(p => p.CanWrite && p.GetCustomAttribute<EnvironmentVariableAttribute>() != null);

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<EnvironmentVariableAttribute>();
                var envValue = Environment.GetEnvironmentVariable(attribute!.Name);
                if (envValue != null)
                {
                    var convertedValue = Convert.ChangeType(envValue, property.PropertyType);
                    property.SetValue(option, convertedValue);
                }
            }

            return option;
        }
    }
}
