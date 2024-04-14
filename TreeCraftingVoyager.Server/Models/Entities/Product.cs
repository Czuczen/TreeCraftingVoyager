using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Models.Entities.Shared.EntityBase;

namespace TreeCraftingVoyager.Server.Models.Entities
{
    public class Product : EntityBase
    {
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
