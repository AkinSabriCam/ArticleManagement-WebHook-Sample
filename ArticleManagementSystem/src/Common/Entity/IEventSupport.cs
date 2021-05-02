using System.Collections.Generic;

namespace Common.Entity
{
    public interface IEventSupport
    {
        IReadOnlyList<IEvent> DomainEvents { get; }
        void ClearEvents();
    }
}