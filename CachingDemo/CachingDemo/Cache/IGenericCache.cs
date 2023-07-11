namespace CachingDemo.Cache
{
    public interface IGenericCache
    {
        public Task<T> GetOrSet<T>(string key, Func<Task<T>> getData);
        public Task RemoveItem(string key);
    }
}
