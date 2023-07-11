using Microsoft.Extensions.Caching.Memory;

namespace CachingDemo.Cache
{
    public class InMemoryGenericCache : IGenericCache
    {
        readonly IMemoryCache cache;
        public InMemoryGenericCache(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public async Task<T> GetOrSet<T>(string key, Func<Task<T>> getData)
        {
            return await cache.GetOrCreateAsync(key, async (e) => await getData());
        }

        public async Task RemoveItem(string key)
        {
            cache.Remove(key);
        }
    }
}
