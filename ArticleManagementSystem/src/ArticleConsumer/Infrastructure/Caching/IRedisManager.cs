using System;
using System.Threading.Tasks;

namespace ArticleConsumer.Infrastructure.Caching
{
    public interface IRedisManager
    {
        Task<T> GetAsync<T>(string key) where T : class;
        Task<bool> SetAsync<T>(string key, T value) where T : class;
        Task<T> GetOrRunAsync<T>(string key, Func<Task<T>> action) where T : class;
    }
}