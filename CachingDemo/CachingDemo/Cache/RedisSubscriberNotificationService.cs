using StackExchange.Redis;

namespace CachingDemo.Cache
{
    public class RedisSubscriberNotificationService : BackgroundService
    {
        readonly IEnumerable<IRedisSubscriber> subscribers;
        readonly ConnectionMultiplexer redis;
        public RedisSubscriberNotificationService(IEnumerable<IRedisSubscriber> subscribers, ConnectionMultiplexer redis)
        {
            this.redis = redis;
            this.subscribers = subscribers;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            if (subscribers.Any())
            {
                var sub = redis.GetSubscriber();
                sub.Subscribe("__keyevent@0__:del", async (c, m) =>
                {
                    await Task.WhenAll(subscribers.Select(async subscriber => await subscriber.OnItemRemoved(m)));
                });
            }
        }
    }
}
