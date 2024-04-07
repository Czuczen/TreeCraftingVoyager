using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase
{
    public interface IEntityBase<TPrimaryKey>
    {
        [Key]
        public TPrimaryKey Id { get; set; }
    }
}
