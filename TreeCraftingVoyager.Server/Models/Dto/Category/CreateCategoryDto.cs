using System.ComponentModel.DataAnnotations;
using TreeCraftingVoyager.Server.Attributes;
using TreeCraftingVoyager.Server.Attributes.Validation;

namespace TreeCraftingVoyager.Server.Models.Dto.Category;

public class CreateCategoryDto
{
    [Required(ErrorMessage = "Pole jest wymagane")]
    [StringLength(50, MinimumLength = 4, ErrorMessage = "Nazwa nie może być krótsza niż 4 i dłuższa niż 50")]
    [SecurityValidate]
    public string Name { get; set; }

    [StringLength(250, ErrorMessage = "Opis nie może przekraczać 250 znaków")]
    [SecurityValidate]
    public string Description { get; set; }

    [Url(ErrorMessage = "Podaj prawidłowy adres url (np. https://www.examplesite.pl)")]
    public string ImageURL { get; set; }

    [Required(ErrorMessage = "Pole jest wymagane")]
    [Range(1, 100, ErrorMessage = "Wybierz między 1 a 100")]
    public int DisplayOrder { get; set; }

    public long? ParentId { get; set; }
}