using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

public interface IEntityBase<TPrimaryKey>
{
    [Key]
    public TPrimaryKey Id { get; set; }
}

public interface IEntityBase : IEntityBase<long>
{
    [Key]
    public long Id { get; set; }
}
