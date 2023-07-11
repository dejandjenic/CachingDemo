namespace CachingDemo.Cache
{
    public interface IRedisSubscriber
    {
        Task OnItemRemoved(string key);
    }
}
