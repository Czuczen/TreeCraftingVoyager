using Microsoft.EntityFrameworkCore;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Data.SeedData
{
    public class CategoryDataSeeder : ISeeder
    {
        public int Order => 1;

        private readonly List<Category> _categories = new()
        {
            new Category
            {
                Id = 1,
                Name = "Elektronika",
                Description = "",
                ImageURL = "",
                SEOKeywords = ""
            },
            new Category
            {
                Id = 2,
                Name = "Ogród",
                Description = "",
                ImageURL = "",
                SEOKeywords = ""
            },
        };

        public void Seed(ModelBuilder modelBuilder)
        {
            foreach (var league in _categories)
                modelBuilder.Entity<Category>().HasData(league);
        }
    }
}
