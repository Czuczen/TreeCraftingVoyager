namespace TreeCraftingVoyager.Server.Models.ViewModels.Product;

public class ProductDetailsViewModel
{
    public long Id{ get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public string CategoryName { get; set; }
}