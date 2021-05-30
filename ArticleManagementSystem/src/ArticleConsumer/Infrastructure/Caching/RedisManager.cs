using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace ArticleConsumer.Infrastructure.Caching
{
    public class RedisManager : IRedisManager
    {
        private readonly IDatabase _db;
        private readonly IRedisCacheDbProvider _dbProvider;
        private readonly ILogger<RedisManager> _logger;


        public RedisManager(IRedisCacheDbProvider dbProvider, ILogger<RedisManager> logger)
        {
            _logger = logger;
            _dbProvider = dbProvider;
            _db = _dbProvider.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var valueBytes = await _db.StringGetAsync(key, CommandFlags.PreferMaster);
            if(!valueBytes.HasValue)
                return null;

            var stringValue = Encoding.UTF8.GetString(valueBytes);
            return JsonSerializer.Deserialize<T>(stringValue);
        }

        public Task<bool> SetAsync<T>(string key, T value) where T : class
        {
            var jsonString = JsonSerializer.Serialize(value);
            var redisValue = Encoding.UTF8.GetBytes(jsonString);

            return _db.StringSetAsync(key, redisValue);
        }

        public async Task<T> GetOrRunAsync<T>(string key, Func<Task<T>> action) where T : class
        {
            var result = await GetAsync<T>(key);

            if(result == null)
            {
                var invokeResult = await action.Invoke();
                if (invokeResult != null)
                    await SetAsync(key, invokeResult);

                return invokeResult;
            }

            return result;
        }
    }
}