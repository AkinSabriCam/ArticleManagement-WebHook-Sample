using System;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace ArticleConsumer.Infrastructure.Caching
{
    public class RedisCacheDbProvider : IRedisCacheDbProvider
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;
        private readonly RedisSettingsModel redisSettingsModel;

        public RedisCacheDbProvider(IOptions<RedisSettingsModel> redisSettings)
        {
            redisSettingsModel = redisSettings.Value;

            var redisConfigurations = GetConfigurationOptions(redisSettingsModel);
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConfigurations));
        }

        public IDatabase GetDatabase()
        {
            return _connectionMultiplexer.Value.GetDatabase(redisSettingsModel.DefaultDatabase);
        }

        private static ConfigurationOptions GetConfigurationOptions(RedisSettingsModel redisOptions)
        {
            return new ConfigurationOptions
            {
                EndPoints = { redisOptions.EndPoint },
                Password = redisOptions.Password,
                AllowAdmin = true,
                DefaultDatabase = redisOptions.DefaultDatabase,
                KeepAlive = 60,
                SyncTimeout = redisOptions.SyncTimeout,
                ConnectTimeout = redisOptions.ConnectTimeout
            };
        }
    }
}