using System.Linq;
using System;
using System.Threading.Tasks;
using Common.Entity;
using Common.Event;
using Common.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Infrastructure.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;
        private readonly IDomainEventPublisher _domainPublisher;

        public UnitOfWork(AppDbContext dbContext, ILogger<UnitOfWork> logger, IDomainEventPublisher domainPublisher)
        {
            _dbContext = dbContext;
            _logger = logger;
            _domainPublisher = domainPublisher;
        }

        public async Task RunInDbTransaction(Func<Task> action)
        {
            var strategy = _dbContext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    await action();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError($"Error in database transaction {ex.Message}");
                }
            });
        }

        public async Task SaveChangesAsync()
        {
            var domainEvent = GetDomainEvents();
            
            await _dbContext.SaveChangesAsync();
            await _domainPublisher.PublishAllAsync(domainEvent);
        }

        private List<IEvent> GetDomainEvents()
        {
            var entities = _dbContext.ChangeTracker.Entries<IEventSupport>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any())
                .ToList();

            return entities.SelectMany(x => x.DomainEvents).ToList();
        }
    }
}