using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Attributes;

namespace TreeCraftingVoyager.Server.Models.Dto.Product
{
    public class CreateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "asdasdasdasdasdasdasd.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0.1, 100000)]
        public decimal Price { get; set; }

        [MinimumNullableCurrentDateTime]
        public DateTime? ExpirationDate { get; set; }

        [Required(ErrorMessage = "eeeeeeeeeeeeeee")]
        [Range(1, long.MaxValue, ErrorMessage = "Wybierz kategorię")]
        public long CategoryId { get; set; }
    }
}
