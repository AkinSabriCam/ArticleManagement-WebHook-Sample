using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Event
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync(IEvent @event);
        Task PublishAllAsync(List<IEvent> events);

    }
}