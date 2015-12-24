using System.Threading.Tasks;

namespace FanSelector.Cache
{
    public interface IRedisRepository
    {
        Task<bool> Add<T>(string key, T value) where T : class;

        Task<T> Get<T>(string key) where T : class;

        Task<bool> Remove(string key);

        Task<bool> Exists(string key);
    }
}