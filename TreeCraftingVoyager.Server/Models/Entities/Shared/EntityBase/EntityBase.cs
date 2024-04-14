using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

public abstract class EntityBase<TPrimaryKey> : IEntityBase<TPrimaryKey>
{
    [Key]
    public TPrimaryKey Id { get; set; }
}

public abstract class EntityBase : IEntityBase
{
    [Key]
    public long Id { get; set; }
}
