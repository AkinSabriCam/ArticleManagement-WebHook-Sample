using System;
using System.Threading.Tasks;

namespace Common.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();

        Task RunInDbTransaction(Func<Task> action);
    }
}