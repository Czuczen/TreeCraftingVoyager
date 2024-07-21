using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Attributes;
using TreeCraftingVoyager.Server.Attributes.Validation;
using TreeCraftingVoyager.Server.Models.Dto.Shared.EntityDto;

namespace TreeCraftingVoyager.Server.Models.Dto.Product;

public class UpdateProductDto : EntityDto
{
    [Required(ErrorMessage = "Pole jest wymagane")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Nazwa nie może być krótsza niż 4 i dłuższa niż 100")]
    [SecurityValidate]
    public string Name { get; set; }

    [StringLength(1000, ErrorMessage = "Opis nie może przekraczać 1000 znaków")]
    [SecurityValidate]
    public string Description { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane")]
    [Range(0.01, 100000, ErrorMessage = "Wybierz między 0.01 a 100000")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane")]
    [Range(1, long.MaxValue, ErrorMessage = "Wybierz kategorię")]
    public long CategoryId { get; set; }
}