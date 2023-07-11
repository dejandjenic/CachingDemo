using CachingDemo.Cache;
using CachingDemo.Model;
using CachingDemo.Repository;
using Microsoft.Extensions.Caching.Memory;

namespace CachingDemo.Repository
{
    public class InMemoryDataRepository : InMemoryGenericCache, IDataRepository
    {
        readonly IDataRepository repository;
        public InMemoryDataRepository(IDataRepository repository, IMemoryCache cache) : base(cache)
        {
            this.repository = repository;
        }

        public async Task Add(string name)
        {
            await RemoveItem($"SearchEntities_{name}");
            await repository.Add(name);
        }

        public async Task<IList<DataEntity>> SearchEntities(string name)
        {
            return await GetOrSet($"SearchEntities_{name}", () => repository.SearchEntities(name));
        }
    }
}
