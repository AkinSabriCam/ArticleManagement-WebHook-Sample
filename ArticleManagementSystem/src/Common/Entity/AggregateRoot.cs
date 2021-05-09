using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Common.Event;

namespace Common.Entity
{
    public class AggregateRoot<TId> : IEntity<TId>, IModifiableEntity, IEventSupport where TId : IEquatable<TId>
    {
        private readonly List<IEvent> _domainEvents = new List<IEvent>();

        [JsonIgnore]
        public IReadOnlyList<IEvent> DomainEvents { get { return _domainEvents; } }
        public TId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UpdatedBy { get; set; }

        protected void AddDomainEvent(IEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }

        public void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }

    public class AggregateRoot : AggregateRoot<Guid>
    {
        public AggregateRoot()
        {
            Id = Guid.NewGuid();
        }
    }
}