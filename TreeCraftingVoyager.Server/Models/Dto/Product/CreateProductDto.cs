namespace TreeCraftingVoyager.Server.Models.Dto.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
