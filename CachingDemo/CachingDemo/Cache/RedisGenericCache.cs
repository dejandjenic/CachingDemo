using StackExchange.Redis;

namespace CachingDemo.Cache
{
    public class RedisGenericCache : IGenericCache
    {
        readonly IDatabase db;

        public RedisGenericCache(ConnectionMultiplexer redis)
        {            
            db = redis.GetDatabase();
        }

        public async Task<T> GetOrSet<T>(string key, Func<Task<T>> getData)
        {
            var cacheData = await db.StringGetAsync(key);

            if (!cacheData.HasValue)
            {
                var data = await getData();
                var cacheString = System.Text.Json.JsonSerializer.Serialize(data);
                await db.StringSetAsync(key, cacheString);
                cacheData = cacheString;
            }
            return System.Text.Json.JsonSerializer.Deserialize<T>(cacheData);
        }

        public async Task RemoveItem(string key)
        {
            await db.StringGetDeleteAsync(key);
        }
    }
}
