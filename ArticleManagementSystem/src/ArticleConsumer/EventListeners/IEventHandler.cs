using System.Threading.Tasks;
using Common.Event;

namespace ArticleConsumer.EventListeners
{
    public interface IEventHandler<TEvent> where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }
}