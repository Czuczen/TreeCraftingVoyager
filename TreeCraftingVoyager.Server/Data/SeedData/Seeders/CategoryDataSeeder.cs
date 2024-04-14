using Microsoft.EntityFrameworkCore;
using TreeCraftingVoyager.Server.Models.Entities;

namespace TreeCraftingVoyager.Server.Data.SeedData.Seeders;

public class CategoryDataSeeder : ISeeder
{
    public int Order => 1;

    private readonly List<Category> _categories = new()
    {
        // ===== root start ======
        new Category
        {
            Id = 1,
            Name = "Elektronika",
            Description = "root",
            DisplayOrder = 3,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 2,
            Name = "Dom i ogród",
            Description = "root",
            DisplayOrder = 4,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 3,
            Name = "Super market",
            Description = "root",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 4,
            Name = "Uroda",
            Description = "root",
            DisplayOrder = 5,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 5,
            Name = "Zdrowie",
            Description = "root",
            DisplayOrder = 6,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 6,
            Name = "Motoryzacja",
            Description = "root",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ===== root end ======

        // ==============================
        // ===== first childs start ======

        // ----- elektronika start -----
        new Category
        {
            Id = 7,
            Name = "Telefony i akcesoria",
            ParentId = 1,
            Description = "first child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 8,
            Name = "Komputery",
            ParentId = 1,
            Description = "first child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 9,
            Name = "AGD",
            ParentId = 1,
            Description = "first child",
            DisplayOrder = 3,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- elektronika end -----

        // ----- Dom i ogród start -----
        new Category
        {
            Id = 10,
            Name = "Wyposażenie",
            ParentId = 2,
            Description = "first child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 11,
            Name = "Narzędzia",
            ParentId = 2,
            Description = "first child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 12,
            Name = "Ogród",
            ParentId = 2,
            Description = "first child",
            DisplayOrder = 3,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Dom i ogród end -----

        // ----- Super market start -----
        new Category
        {
            Id = 13,
            Name = "Produkty spożywcze",
            ParentId = 3,
            Description = "first child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 14,
            Name = "Artykuły dla zwierząt",
            ParentId = 3,
            Description = "first child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 15,
            Name = "Utrzymanie czystości",
            ParentId = 3,
            Description = "first child",
            DisplayOrder = 3,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Super market end -----

        // ----- Uroda start -----
        new Category
        {
            Id = 16,
            Name = "Pielęgnacja",
            ParentId = 4,
            Description = "first child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 17,
            Name = "Makijaż",
            ParentId = 4,
            Description = "first child",
            DisplayOrder = 3,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 18,
            Name = "Perfumy",
            ParentId = 4,
            Description = "first child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Uroda end -----

        // ----- Zdrowie start -----
        new Category
        {
            Id = 19,
            Name = "Domowa apteczka",
            ParentId = 5,
            Description = "first child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 20,
            Name = "Urządzenia medyczne",
            ParentId = 5,
            Description = "first child",
            DisplayOrder = 3,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 21,
            Name = "Medycyna naturalna",
            ParentId = 5,
            Description = "first child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Zdrowie end -----

        // ----- Motoryzacja start -----
        new Category
        {
            Id = 22,
            Name = "Opony i felgi",
            ParentId = 6,
            Description = "first child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 23,
            Name = "Części samochodowe",
            ParentId = 6,
            Description = "first child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 24,
            Name = "Warsztat",
            ParentId = 6,
            Description = "first child",
            DisplayOrder = 3,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Motoryzacja end -----

        // ==============================
        // ===== first childs end ======
        // ============================================================
        // ==============================
        // ===== second childs start ======

        // ----- Telefony i akcesoria start -----
        new Category
        {
            Id = 25,
            Name = "Smartfony",
            ParentId = 7,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 26,
            Name = "Tablety",
            ParentId = 7,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Telefony i akcesoria end -----

        // ----- Komputery start -----
        new Category
        {
            Id = 27,
            Name = "Laptopy",
            ParentId = 8,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 28,
            Name = "Komputery stacjonarne",
            ParentId = 8,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Komputery end -----

        // ----- AGD start -----
        new Category
        {
            Id = 29,
            Name = "AGD małe",
            ParentId = 9,
            Description = "second child",
            DisplayOrder = 3,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 30,
            Name = "AGD do zabudowy",
            ParentId = 9,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 31,
            Name = "AGD",
            ParentId = 9,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- AGD end -----

        // ----- Wyposażenie start -----
        new Category
        {
            Id = 32,
            Name = "Garnki i patelnie",
            ParentId = 10,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 33,
            Name = "Dekoracje i ozdoby",
            ParentId = 10,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Wyposażenie end -----

        // ----- Narzędzia start -----
        new Category
        {
            Id = 34,
            Name = "Piły i pilarki",
            ParentId = 11,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 35,
            Name = "Odkurzacze przemysłowe",
            ParentId = 11,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Narzędzia end -----

        // ----- Ogród start -----
        new Category
        {
            Id = 36,
            Name = "Nawozy i preparaty",
            ParentId = 12,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 37,
            Name = "Meble ogrodowe",
            ParentId = 12,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Ogród end -----

        // ----- Produkty spożywcze start -----
        new Category
        {
            Id = 38,
            Name = "Kawy",
            ParentId = 13,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 39,
            Name = "Herbaty",
            ParentId = 13,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Produkty spożywcze end -----

        // ----- Artykuły dla zwierząt start -----
        new Category
        {
            Id = 40,
            Name = "Karmy",
            ParentId = 14,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 41,
            Name = "Legowiska",
            ParentId = 14,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Artykuły dla zwierząt end -----

        // ----- Utrzymanie czystości start -----
        new Category
        {
            Id = 42,
            Name = "Środki czyszczące",
            ParentId = 15,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 43,
            Name = "Suszarki na pranie",
            ParentId = 15,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Utrzymanie czystości end -----

            
        // ----- Pielęgnacja start -----
        new Category
        {
            Id = 44,
            Name = "Kremy do twarzy",
            ParentId = 16,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 45,
            Name = "Szampony",
            ParentId = 16,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Pielęgnacja end -----

            
        // ----- Makijaż start -----
        new Category
        {
            Id = 46,
            Name = "Makijaż ust",
            ParentId = 17,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 47,
            Name = "Sztuczne rzęsy",
            ParentId = 17,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Makijaż end -----

            
        // ----- Perfumy start -----
        new Category
        {
            Id = 48,
            Name = "Zestawy",
            ParentId = 18,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 49,
            Name = "Wody toaletowe",
            ParentId = 18,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Perfumy end -----

            
        // ----- Domowa apteczka start -----
        new Category
        {
            Id = 50,
            Name = "Suplementy diety",
            ParentId = 19,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 51,
            Name = "Leki bez recepty",
            ParentId = 19,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Domowa apteczka end -----

            
        // ----- Urządzenia medyczne start -----
        new Category
        {
            Id = 52,
            Name = "Inhalatory",
            ParentId = 20,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 53,
            Name = "Termometry",
            ParentId = 20,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Urządzenia medyczne end -----

        // ----- Medycyna naturalna start -----
        new Category
        {
            Id = 54,
            Name = "Produkty konopne",
            ParentId = 21,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 55,
            Name = "Olejki eteryczne",
            ParentId = 21,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Medycyna naturalna end -----

        // ----- Opony i felgi start -----
        new Category
        {
            Id = 56,
            Name = "Felgi aluminiowe",
            ParentId = 22,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 57,
            Name = "Do motocykli",
            ParentId = 22,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Opony i felgi end -----

        // ----- Części samochodowe start -----
        new Category
        {
            Id = 58,
            Name = "Układ zawieszenia",
            ParentId = 23,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 59,
            Name = "Części karoserii",
            ParentId = 23,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Części samochodowe end -----

        // ----- Warsztat start -----
        new Category
        {
            Id = 60,
            Name = "Zestawy narzędzi",
            ParentId = 24,
            Description = "second child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 61,
            Name = "Klucze",
            ParentId = 24,
            Description = "second child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- Warsztat end -----

        // ==============================
        // ===== second childs end ======
        // ============================================================
        // ==============================
        // ===== third childs start ======

        // ----- AGD małe start -----
        new Category
        {
            Id = 62,
            Name = "Odkurzacze pionowe",
            ParentId = 29,
            Description = "third child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 63,
            Name = "Do kuchni",
            ParentId = 29,
            Description = "third child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- AGD małe end -----

        // ----- AGD do zabudowy start -----
        new Category
        {
            Id = 64,
            Name = "Płyty grzewcze",
            ParentId = 30,
            Description = "third child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 65,
            Name = "Okapy",
            ParentId = 30,
            Description = "third child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- AGD do zabudowy end -----

        // ----- AGD start -----
        new Category
        {
            Id = 66,
            Name = "Lodówki",
            ParentId = 31,
            Description = "third child",
            DisplayOrder = 1,
            ImageURL = "https://www.examplesite.pl"
        },
        new Category
        {
            Id = 67,
            Name = "Pralki",
            ParentId = 31,
            Description = "third child",
            DisplayOrder = 2,
            ImageURL = "https://www.examplesite.pl"
        },
        // ----- AGD end -----

        // ==============================
        // ===== third childs end ======
    };

    public void Seed(ModelBuilder modelBuilder)
    {
        foreach (var category in _categories)
            modelBuilder.Entity<Category>().HasData(category);
    }
}