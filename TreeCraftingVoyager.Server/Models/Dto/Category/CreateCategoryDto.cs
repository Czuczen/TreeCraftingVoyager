using System.ComponentModel.DataAnnotations;

namespace TreeCraftingVoyager.Server.Models.Dto.Category
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Nie może byc dłuższy niż 50 i krótrzy niż 4")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Nie może byc dłuższy niż 250")]
        public string Description { get; set; }

        [Url]
        public string ImageURL { get; set; }

        [Required]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }

        public long? ParentId { get; set; }
    }
}
