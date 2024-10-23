namespace Skillup.Shared.Abstractions.Storage
{
    public interface IMemoryStorage
    {
        void Set<T>(string key, T value, TimeSpan? duration = null);
        T Get<T>(string key);
    }
}
