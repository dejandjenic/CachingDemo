namespace CachingDemo.Cache
{
    public class RedisSubscriber : IRedisSubscriber
    {
        readonly InMemoryGenericCache inMemoryGenericCache;
        readonly ILogger<RedisSubscriber> logger;
        public RedisSubscriber(InMemoryGenericCache inMemoryGenericCache,ILogger<RedisSubscriber> logger)
        {
            this.logger = logger;
            this.inMemoryGenericCache = inMemoryGenericCache;
        }
        public async Task OnItemRemoved(string key)
        {
            logger.LogInformation($"redis key deleted {key}");
            await inMemoryGenericCache.RemoveItem(key);
        }
    }
}
