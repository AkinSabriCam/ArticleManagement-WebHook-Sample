using System;
namespace Common.Entity
{
    public class BaseEntity<TId> : IEntity<TId>, IModifiableEntity where TId : IEquatable<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UpdatedBy { get; set; }

    }

    public class BaseEntity : BaseEntity<Guid>
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}