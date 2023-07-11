using CachingDemo.Model;

namespace CachingDemo.Repository
{
    public interface IDataRepository
    {
        Task<IList<DataEntity>> SearchEntities(string name);
        Task Add(string name);
    }
}
