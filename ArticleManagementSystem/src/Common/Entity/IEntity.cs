using System;
namespace Common.Entity
{
    public interface IEntity<TId> where TId : IEquatable<TId>
    {
        TId Id { get; set; }
    }
}