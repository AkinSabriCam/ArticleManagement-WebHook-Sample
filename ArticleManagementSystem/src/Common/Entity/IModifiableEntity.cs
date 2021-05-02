using System;
namespace Common.Entity
{
    public interface IModifiableEntity
    {
        DateTime CreatedDate { get; set; }
        Guid CreatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        Guid UpdatedBy { get; set; }
    }
}