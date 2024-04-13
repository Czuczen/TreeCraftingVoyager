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
                Description = "Elektronika",
                Price = (decimal) 1.2,
                ExpirationDate = DateTime.UtcNow.AddDays(1000),
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Coś do domu i ogrodu",
                Description = "Dom i ogród",
                Price = (decimal) 20.30,
                ExpirationDate = DateTime.UtcNow.AddDays(100),
                CategoryId = 2
            },
            new Product
            {
                Id = 3,
                Name = "Coś z super marketu",
                Description = "Super market",
                Price = (decimal) 2.2,
                ExpirationDate = DateTime.UtcNow.AddDays(2),
                CategoryId = 3
            },
            new Product
            {
                Id = 4,
                Name = "Coś dla urody",
                Description = "Uroda",
                Price = (decimal) 30.2,
                ExpirationDate = DateTime.UtcNow.AddDays(25),
                CategoryId = 4
            },
            new Product
            {
                Id = 5,
                Name = "Coś dla zdrowia",
                Description = "Zdrowie",
                Price = (decimal) 11.2,
                ExpirationDate = DateTime.UtcNow.AddDays(30),
                CategoryId = 5
            },
            new Product
            {
                Id = 6,
                Name = "Coś dla motoryzacji",
                Description = "Motoryzacja",
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
                Name = "Jakiś tam IPhone",
                Description = "Smartfony",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 25
            },
            new Product
            {
                Id = 26,
                Name = "Jakiś tam tablet",
                Description = "Tablety",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 26
            },
            // ----- Telefony i akcesoria end -----

            
            // ----- Komputery start -----
            new Product
            {
                Id = 27,
                Name = "Jakiś tam laptop acer",
                Description = "Laptopy",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 27
            },
            new Product
            {
                Id = 28,
                Name = "Jakiś tam stacjonarny",
                Description = "Komputery stacjonarne",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 28
            },
            // ----- Komputery end -----

            // ----- AGD start -----
            new Product
            {
                Id = 29,
                Name = "Jakieś tam małe agd",
                Description = "AGD małe",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 29
            },
            new Product
            {
                Id = 30,
                Name = "Jakaś tam agd zabudowa",
                Description = "AGD do zabudowy",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 30
            },
            new Product
            {
                Id = 31,
                Name = "Jakiś tam odkurzacz agd",
                Description = "AGD",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 31
            },
            // ----- AGD end -----

            // ----- Wyposażenie start -----
            new Product
            {
                Id = 32,
                Name = "Jakiś tam garnek",
                Description = "Garnki i patelnie",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 32
            },
            new Product
            {
                Id = 33,
                Name = "Jakaś tam dekoracja",
                Description = "Dekoracje i ozdoby",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 33
            },
            // ----- Wyposażenie end -----

            // ----- Narzędzia start -----
            new Product
            {
                Id = 34,
                Name = "Jakaś tam piła",
                Description = "Piły i pilarki",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 34
            },
            new Product
            {
                Id = 35,
                Name = "Jakiś tam przemysłowy odkurzacz",
                Description = "Odkurzacze przemysłowe",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 35
            },
            // ----- Narzędzia end -----

            // ----- Ogród start -----
            new Product
            {
                Id = 36,
                Name = "Jakiś tam nawóz",
                Description = "Nawozy i preparaty",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 36
            },
            new Product
            {
                Id = 37,
                Name = "Jakiś tam mebel do ogrodu",
                Description = "Meble ogrodowe",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 37
            },
            // ----- Ogród end -----

            // ----- Produkty spożywcze start -----
            new Product
            {
                Id = 38,
                Name = "Jakaś tam kawa",
                Description = "Kawy",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 38
            },
            new Product
            {
                Id = 39,
                Name = "Jakaś tam herbata",
                Description = "Herbaty",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 39
            },
            // ----- Produkty spożywcze end -----

            // ----- Artykuły dla zwierząt start -----
            new Product
            {
                Id = 40,
                Name = "Jakaś tam karma",
                Description = "Karmy",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 40
            },
            new Product
            {
                Id = 41,
                Name = "Jakieś tam legowisko",
                Description = "Legowiska",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 41
            },
            // ----- Artykuły dla zwierząt end -----

            // ----- Utrzymanie czystości start -----
            new Product
            {
                Id = 42,
                Name = "Jakaś tam szmatka",
                Description = "Środki czyszczące",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 42
            },
            new Product
            {
                Id = 43,
                Name = "Jakaś tam suszarka",
                Description = "Suszarki na pranie",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 43
            },
            // ----- Utrzymanie czystości end -----

            // ----- Pielęgnacja start -----
            new Product
            {
                Id = 44,
                Name = "Jakiś tam krem na twarz",
                Description = "Kremy do twarzy",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 44
            },
            new Product
            {
                Id = 45,
                Name = "Jakiś tam szampon",
                Description = "Szampony",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 45
            },
            // ----- Pielęgnacja end -----

            // ----- Makijaż start -----
            new Product
            {
                Id = 46,
                Name = "Jakaś tam szminka",
                Description = "Makijaż ust",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 46
            },
            new Product
            {
                Id = 47,
                Name = "Jakaś tam rzęsa",
                Description = "Sztuczne rzęsy",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 47
            },
            // ----- Makijaż end -----

            // ----- Perfumy start -----
            new Product
            {
                Id = 48,
                Name = "Jakiś tam zestaw perfum",
                Description = "Zestawy",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 48
            },
            new Product
            {
                Id = 49,
                Name = "Jakaś tam woda",
                Description = "Wody toaletowe",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 49
            },
            // ----- Perfumy end -----

            // ----- Domowa apteczka start -----
            new Product
            {
                Id = 50,
                Name = "Jakiś tam suplement",
                Description = "Suplementy diety",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 50
            },
            new Product
            {
                Id = 51,
                Name = "Jakaś tam tabletka",
                Description = "Leki bez recepty",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 51
            },
            // ----- Domowa apteczka end -----

            // ----- Urządzenia medyczne start -----
            new Product
            {
                Id = 52,
                Name = "Jakiś tam inhalator",
                Description = "Inhalatory",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 52
            },
            new Product
            {
                Id = 53,
                Name = "Jakiś tam termometr",
                Description = "Termometry",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 53
            },
            // ----- Urządzenia medyczne end -----

            // ----- Medycyna naturalna start -----
            new Product
            {
                Id = 54,
                Name = "Jakaś tam roślinka CBD",
                Description = "Produkty konopne",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 54
            },
            new Product
            {
                Id = 55,
                Name = "Jakiś tam olejek",
                Description = "Olejki eteryczne",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 55
            },
            // ----- Medycyna naturalna end -----

            // ----- Opony i felgi start -----
            new Product
            {
                Id = 56,
                Name = "Jakaś tam felga alu",
                Description = "Felgi aluminiowe",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 56
            },
            new Product
            {
                Id = 57,
                Name = "Jakieś tam motory",
                Description = "Do motocykli",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 57
            },
            // ----- Opony i felgi end -----

            // ----- Części samochodowe start -----
            new Product
            {
                Id = 58,
                Name = "Jakaś tam sprężyna ",
                Description = "Układ zawieszenia",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 58
            },
            new Product
            {
                Id = 59,
                Name = "Jakaś tam maska",
                Description = "Części karoserii",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 59
            },
            // ----- Części samochodowe end -----

            // ----- Warsztat start -----
            new Product
            {
                Id = 60,
                Name = "Jakaś tam paczka narzędzi",
                Description = "Zestawy narzędzi",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 60
            },
            new Product
            {
                Id = 61,
                Name = "Jakiś tam klucz",
                Description = "Klucze",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 61
            },
            // ----- Warsztat end -----

            // ==============================
            // ===== second childs products end ======
            // ============================================================
            // ==============================
            // ===== third childs products start ======

            // ----- AGD małe start -----
            new Product
            {
                Id = 62,
                Name = "Jakiś tam odkurzacz pionowy 2000",
                Description = "Odkurzacze pionowe",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 62
            },
            new Product
            {
                Id = 63,
                Name = "Jakiś tam zlew",
                Description = "Do kuchni",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 63
            },
            // ----- AGD małe end -----

            // ----- AGD do zabudowy start -----
            new Product
            {
                Id = 64,
                Name = "Jakaś tam grzałka",
                Description = "Płyty grzewcze",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 64
            },
            new Product
            {
                Id = 65,
                Name = "Jakiś tam okap",
                Description = "Okapy",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 65
            },
            // ----- AGD do zabudowy end -----

            // ----- AGD start -----
            new Product
            {
                Id = 66,
                Name = "Jakaś tam lodówka ",
                Description = "Lodówki",
                Price = (decimal) 112.2,
                ExpirationDate = DateTime.UtcNow.AddDays(123),
                CategoryId = 66
            },
            new Product
            {
                Id = 67,
                Name = "Jakaś tam pralka",
                Description = "Pralki",
                Price = (decimal) 234.1,
                ExpirationDate = DateTime.UtcNow.AddDays(11),
                CategoryId = 67
            },
            // ----- AGD end -----

            // ==============================
            // ===== third childs product end ======
        };

        public void Seed(ModelBuilder modelBuilder)
        {
            foreach (var product in _products)
                modelBuilder.Entity<Product>().HasData(product);
        }
    }
}
