using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FanSelector.Cache.Impl
{

    public class RedisRepository : IRedisRepository
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection =
            new Lazy<ConnectionMultiplexer>(
                () =>
                    ConnectionMultiplexer.Connect(
                        "fsapi.redis.cache.windows.net,abortConnect=false,ssl=true,password=DOxvqvoNZPNR81d7oH/znYbfUCDPFftHToi5xgcY21g="));

        public static ConnectionMultiplexer Connection => LazyConnection.Value;
        readonly IDatabase _cache = Connection.GetDatabase();

        public async Task<bool> Add<T>(string key, T value) where T : class
        {
            var serializedObject = JsonConvert.SerializeObject(value);

            return await _cache.StringSetAsync(key, serializedObject);
        }

        public async Task<T> Get<T>(string key) where T : class
        {
            var serializedObject = await _cache.StringGetAsync(key);
            if (!serializedObject.HasValue)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<T>(serializedObject);
        }

        public async Task<bool> Remove(string key)
        {
            return await _cache.KeyDeleteAsync(key);
        }

        public async Task<bool> Exists(string key)
        {
            return await _cache.KeyExistsAsync(key);
        }
    }
}