using Microsoft.EntityFrameworkCore;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Data.SeedData.Seeders
{
    public class ProductDataSeeder : ISeeder
    {
        public int Order => 2;

        private readonly List<Product> _products = new()
        {
            // ===== root products start ======
            new Product
            {
                Id = 1,
                Name = "Coś elektronicznego",
                Description = "Jakiś tam produkt elektroniczny",
                Price = (decimal) 1.2,
                ExpirationDate = DateTime.UtcNow.AddDays(1000),
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Coś do domu i ogrodu",
                Description = "Jakiś tam produkt do domu i ogrodu",
                Price = (decimal) 20.30,
                ExpirationDate = DateTime.UtcNow.AddDays(100),
                CategoryId = 2
            },
            new Product
            {
                Id = 3,
                Name = "Coś z super marketu",
                Description = "Jakiś tam produkt z super marketu",
                Price = (decimal) 2.2,
                ExpirationDate = DateTime.UtcNow.AddDays(2),
                CategoryId = 3
            },
            new Product
            {
                Id = 4,
                Name = "Coś dla urody",
                Description = "Jakiś tam produkt dla urody",
                Price = (decimal) 30.2,
                ExpirationDate = DateTime.UtcNow.AddDays(25),
                CategoryId = 4
            },
            new Product
            {
                Id = 5,
                Name = "Coś dla zdrowia",
                Description = "Jakiś tam produkt dla zdrowia",
                Price = (decimal) 11.2,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                CategoryId = 5
            },
            new Product
            {
                Id = 6,
                Name = "Coś dla motoryzacji",
                Description = "Jakiś tam produkt dla motoryzacji",
                Price = (decimal) 1123.2,
                ExpirationDate = DateTime.UtcNow.AddDays(300),
                CategoryId = 6
            },
            // ===== root products end ======

            // ========================================
            // ===== first childs products start ======

            // ----- elektronika start -----
            new Product
            {
                Id = 7,
                Name = "Jakiś tam telefon",
                Description = "Telefony i akcesoria",
                Price = (decimal) 1.2,
                ExpirationDate = DateTime.UtcNow.AddDays(23),
                CategoryId = 7
            },
            new Product
            {
                Id = 8,
                Name = "Jakiś tam komputer",
                Description = "Komputery",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(34),
                CategoryId = 8
            },
            new Product
            {
                Id = 9,
                Name = "Jakaś tam lodówka",
                Description = "AGD",
                Price = (decimal) 112.26,
                ExpirationDate = DateTime.UtcNow.AddDays(56),
                CategoryId = 9
            },
            // ----- elektronika end -----

            // ----- Dom i ogród start -----
            new Product
            {
                Id = 10,
                Name = "Jakiś tam obraz",
                Description = "Wyposażenie",
                Price = (decimal) 112.22,
                ExpirationDate = DateTime.UtcNow.AddDays(360),
                CategoryId = 10
            },
            new Product
            {
                Id = 11,
                Name = "Jakiś tam młotek",
                Description = "Narzędzia",
                Price = (decimal) 112.24,
                ExpirationDate = DateTime.UtcNow.AddDays(44),
                CategoryId = 11
            },
            new Product
            {
                Id = 12,
                Name = "Jakaś tam doniczka",
                Description = "Ogród",
                Price = (decimal) 1212.2,
                ExpirationDate = DateTime.UtcNow.AddDays(3450),
                CategoryId = 12
            },
            // ----- Dom i ogród end -----

            // ----- Super market start -----
            new Product
            {
                Id = 13,
                Name = "Jakiś tam chleb",
                Description = "Produkty spożywcze",
                Price = (decimal) 1412.2,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 13
            },
            new Product
            {
                Id = 14,
                Name = "Jakaś tam karma ogólna",
                Description = "Artykuły dla zwierząt",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                CategoryId = 14
            },
            new Product
            {
                Id = 15,
                Name = "Jakaś tam miotła",
                Description = "Utrzymanie czystości",
                Price = (decimal) 234,
                ExpirationDate = DateTime.UtcNow.AddDays(23),
                CategoryId = 15
            },
            // ----- Super market end -----

            // ----- Uroda start -----
            new Product
            {
                Id = 16,
                Name = "Jakiś tam krem",
                Description = "Pielęgnacja",
                Price = (decimal) 1412.2,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 16
            },
            new Product
            {
                Id = 17,
                Name = "Jakaś tam podkład",
                Description = "Makijaż",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                CategoryId = 17
            },
            new Product
            {
                Id = 18,
                Name = "Jakaś tam dezodorant",
                Description = "Perfumy",
                Price = (decimal) 234,
                ExpirationDate = DateTime.UtcNow.AddDays(23),
                CategoryId = 18
            },
            // ----- Uroda end -----

            // ----- Zdrowie start -----
            new Product
            {
                Id = 19,
                Name = "Jakiś tam bandarz",
                Description = "Domowa apteczka",
                Price = (decimal) 1412.2,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 19
            },
            new Product
            {
                Id = 20,
                Name = "Jakiś tam rozrusznik serca",
                Description = "Urządzenia medyczne",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(1),
                CategoryId = 20
            },
            new Product
            {
                Id = 21,
                Name = "Jakaś tam herbatka naturalna",
                Description = "Medycyna naturalna",
                Price = (decimal) 234,
                ExpirationDate = DateTime.UtcNow.AddDays(23),
                CategoryId = 21
            },
            // ----- Zdrowie end -----

            // ----- Motoryzacja start -----
            new Product
            {
                Id = 22,
                Name = "Jakieś tam felgi",
                Description = "Opony i felgi",
                Price = (decimal) 1412.2,
                ExpirationDate = DateTime.UtcNow.AddDays(131),
                CategoryId = 22
            },
            new Product
            {
                Id = 23,
                Name = "Jakaś tam lampa",
                Description = "Części samochodowe",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 23
            },
            new Product
            {
                Id = 24,
                Name = "Jakaś tam śrubka",
                Description = "Warsztat",
                Price = (decimal) 234,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 24
            },
            // ----- Motoryzacja end -----


            // ==============================
            // ===== first childs products end ======
            // ============================================================
            // ==============================
            // ===== second childs products start ======

            // ----- Telefony i akcesoria start -----
            new Product
            {
                Id = 25,
                Name = "Jakaś tam ",
                Description = "",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 25
            },
            new Product
            {
                Id = 26,
                Name = "Jakaś tam ",
                Description = "",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 26
            },
            // ----- Telefony i akcesoria end -----
        };

        public void Seed(ModelBuilder modelBuilder)
        {
            foreach (var product in _products)
                modelBuilder.Entity<Product>().HasData(product);
        }
    }
}
