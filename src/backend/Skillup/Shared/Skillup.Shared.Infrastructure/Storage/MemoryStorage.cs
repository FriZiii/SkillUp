using Microsoft.Extensions.Caching.Memory;
using Skillup.Shared.Abstractions.Storage;

namespace Skillup.Shared.Infrastructure.Storage
{
    public class MemoryStorage : IMemoryStorage
    {
        private readonly IMemoryCache _cache;

        public MemoryStorage(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public void Set<T>(string key, T value, TimeSpan? duration = null)
        {
            _cache.Set(key, value, duration ?? TimeSpan.FromSeconds(5));
        }
    }
}
