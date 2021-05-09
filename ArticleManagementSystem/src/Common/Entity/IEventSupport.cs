using System.Collections.Generic;
using Common.Event;

namespace Common.Entity
{
    public interface IEventSupport
    {
        IReadOnlyList<IEvent> DomainEvents { get; }
        void ClearEvents();
    }
}