using StackExchange.Redis;

namespace ArticleConsumer.Infrastructure.Caching
{
    public interface IRedisCacheDbProvider
    {
        IDatabase GetDatabase();
    }
}