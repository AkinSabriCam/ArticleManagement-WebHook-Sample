using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Event;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.MassTransit
{
    public class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IBusControl _bus;
        private readonly ILogger<DomainEventPublisher> _logger;

        public DomainEventPublisher(IBusControl bus, ILogger<DomainEventPublisher> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task PublishAllAsync(List<IEvent> events)
        {
            if (!events.Any())
                await Task.CompletedTask;

            foreach (var @event in events)
                await PublishAsync(@event);
        }

        public async Task PublishAsync(IEvent @event)
        {
            var eventType = @event.GetType();
            var serializedEvent = JsonConvert.SerializeObject(@event);
            try
            {
                _logger.LogInformation($"Domain Event Publishing: {eventType} Event: {serializedEvent}");
                await _bus.Publish(@event, @event.GetType());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Event could not publish: {eventType} Event: {serializedEvent}");
            }
        }
    }
}