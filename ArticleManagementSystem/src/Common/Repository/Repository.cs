using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Entity;
using Microsoft.EntityFrameworkCore;

namespace Common.Repository
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
                    where TEntity : AggregateRoot<TId>, new() where TId : IEquatable<TId>
    {
        protected readonly DbSet<TEntity> _dbTable;

        public Repository(DbContext dbContext)
        {
            _dbTable = dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ValidationException($"Entity can not be null to insert");

            _dbTable.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbTable.Update(entity);
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await _dbTable.AsNoTracking()
                                       .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (entity == null)
                throw new ValidationException($"Entity can not be found by {id} to delete.");

            _dbTable.Remove(entity);
        }


        public Task<List<TEntity>> GetAllAsync()
        {
            return _dbTable.AsNoTracking().ToListAsync();
        }

        public Task<TEntity> GetByIdAsync(TId id)
        {
            return _dbTable.AsNoTracking()
                           .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbTable.AsNoTracking().AnyAsync(predicate);
        }
    }
}