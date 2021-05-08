using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Entity;

namespace Common.Repository
{
    public interface IRepository<TEntity, TId> where TEntity : AggregateRoot<TId>, new() where TId : IEquatable<TId>
    {

        Task<TEntity> GetByIdAsync(TId id);
        Task<TResult> GetByIdAsync<TResult>(TId id) where TResult : class;
        Task<List<TEntity>> GetAllAsync();
        Task<List<TResult>> GetAllAsync<TResult>() where TResult : class;
        Task DeleteAsync(TId id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}