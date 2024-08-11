namespace Skillup.Shared.Infrastructure.EnvironmentInjector
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class EnvironmentVariableAttribute : Attribute
    {
        public string Name { get; }

        public EnvironmentVariableAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Environment variable name cannot be null or empty.", nameof(name));
            }
            Name = name;
        }
    }
}
