using CachingDemo.Model;

namespace CachingDemo.Service
{
    public interface IDataService
    {
        Task<IList<DataEntity>> SearchEntities(string name);
        Task Add(string name);
    }
}
