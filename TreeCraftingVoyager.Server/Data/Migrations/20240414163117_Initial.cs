using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TreeCraftingVoyager.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ImageURL = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DisplayOrder", "ImageURL", "Name", "ParentId" },
                values: new object[,]
                {
                    { 1L, "root", 3, "https://www.examplesite.pl", "Elektronika", null },
                    { 2L, "root", 4, "https://www.examplesite.pl", "Dom i ogród", null },
                    { 3L, "root", 2, "https://www.examplesite.pl", "Super market", null },
                    { 4L, "root", 5, "https://www.examplesite.pl", "Uroda", null },
                    { 5L, "root", 6, "https://www.examplesite.pl", "Zdrowie", null },
                    { 6L, "root", 1, "https://www.examplesite.pl", "Motoryzacja", null },
                    { 7L, "first child", 2, "https://www.examplesite.pl", "Telefony i akcesoria", 1L },
                    { 8L, "first child", 1, "https://www.examplesite.pl", "Komputery", 1L },
                    { 9L, "first child", 3, "https://www.examplesite.pl", "AGD", 1L },
                    { 10L, "first child", 2, "https://www.examplesite.pl", "Wyposażenie", 2L },
                    { 11L, "first child", 1, "https://www.examplesite.pl", "Narzędzia", 2L },
                    { 12L, "first child", 3, "https://www.examplesite.pl", "Ogród", 2L },
                    { 13L, "first child", 1, "https://www.examplesite.pl", "Produkty spożywcze", 3L },
                    { 14L, "first child", 2, "https://www.examplesite.pl", "Artykuły dla zwierząt", 3L },
                    { 15L, "first child", 3, "https://www.examplesite.pl", "Utrzymanie czystości", 3L },
                    { 16L, "first child", 2, "https://www.examplesite.pl", "Pielęgnacja", 4L },
                    { 17L, "first child", 3, "https://www.examplesite.pl", "Makijaż", 4L },
                    { 18L, "first child", 1, "https://www.examplesite.pl", "Perfumy", 4L },
                    { 19L, "first child", 2, "https://www.examplesite.pl", "Domowa apteczka", 5L },
                    { 20L, "first child", 3, "https://www.examplesite.pl", "Urządzenia medyczne", 5L },
                    { 21L, "first child", 1, "https://www.examplesite.pl", "Medycyna naturalna", 5L },
                    { 22L, "first child", 1, "https://www.examplesite.pl", "Opony i felgi", 6L },
                    { 23L, "first child", 2, "https://www.examplesite.pl", "Części samochodowe", 6L },
                    { 24L, "first child", 3, "https://www.examplesite.pl", "Warsztat", 6L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ExpirationDate", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, 1L, "Elektronika", new DateTime(2027, 1, 9, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(4810), "Coś elektronicznego", 1.2m },
                    { 2L, 2L, "Dom i ogród", new DateTime(2024, 7, 23, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5727), "Coś do domu i ogrodu", 20.3m },
                    { 3L, 3L, "Super market", new DateTime(2024, 4, 16, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5731), "Coś z super marketu", 2.2m },
                    { 4L, 4L, "Uroda", new DateTime(2024, 5, 9, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5735), "Coś dla urody", 30.2m },
                    { 5L, 5L, "Zdrowie", new DateTime(2024, 5, 14, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5737), "Coś dla zdrowia", 11.2m },
                    { 6L, 6L, "Motoryzacja", new DateTime(2025, 2, 8, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5748), "Coś dla motoryzacji", 1123.2m }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DisplayOrder", "ImageURL", "Name", "ParentId" },
                values: new object[,]
                {
                    { 25L, "second child", 1, "https://www.examplesite.pl", "Smartfony", 7L },
                    { 26L, "second child", 2, "https://www.examplesite.pl", "Tablety", 7L },
                    { 27L, "second child", 2, "https://www.examplesite.pl", "Laptopy", 8L },
                    { 28L, "second child", 1, "https://www.examplesite.pl", "Komputery stacjonarne", 8L },
                    { 29L, "second child", 3, "https://www.examplesite.pl", "AGD małe", 9L },
                    { 30L, "second child", 2, "https://www.examplesite.pl", "AGD do zabudowy", 9L },
                    { 31L, "second child", 1, "https://www.examplesite.pl", "AGD", 9L },
                    { 32L, "second child", 1, "https://www.examplesite.pl", "Garnki i patelnie", 10L },
                    { 33L, "second child", 2, "https://www.examplesite.pl", "Dekoracje i ozdoby", 10L },
                    { 34L, "second child", 1, "https://www.examplesite.pl", "Piły i pilarki", 11L },
                    { 35L, "second child", 2, "https://www.examplesite.pl", "Odkurzacze przemysłowe", 11L },
                    { 36L, "second child", 1, "https://www.examplesite.pl", "Nawozy i preparaty", 12L },
                    { 37L, "second child", 2, "https://www.examplesite.pl", "Meble ogrodowe", 12L },
                    { 38L, "second child", 2, "https://www.examplesite.pl", "Kawy", 13L },
                    { 39L, "second child", 1, "https://www.examplesite.pl", "Herbaty", 13L },
                    { 40L, "second child", 1, "https://www.examplesite.pl", "Karmy", 14L },
                    { 41L, "second child", 2, "https://www.examplesite.pl", "Legowiska", 14L },
                    { 42L, "second child", 2, "https://www.examplesite.pl", "Środki czyszczące", 15L },
                    { 43L, "second child", 1, "https://www.examplesite.pl", "Suszarki na pranie", 15L },
                    { 44L, "second child", 2, "https://www.examplesite.pl", "Kremy do twarzy", 16L },
                    { 45L, "second child", 1, "https://www.examplesite.pl", "Szampony", 16L },
                    { 46L, "second child", 1, "https://www.examplesite.pl", "Makijaż ust", 17L },
                    { 47L, "second child", 2, "https://www.examplesite.pl", "Sztuczne rzęsy", 17L },
                    { 48L, "second child", 2, "https://www.examplesite.pl", "Zestawy", 18L },
                    { 49L, "second child", 1, "https://www.examplesite.pl", "Wody toaletowe", 18L },
                    { 50L, "second child", 1, "https://www.examplesite.pl", "Suplementy diety", 19L },
                    { 51L, "second child", 2, "https://www.examplesite.pl", "Leki bez recepty", 19L },
                    { 52L, "second child", 2, "https://www.examplesite.pl", "Inhalatory", 20L },
                    { 53L, "second child", 1, "https://www.examplesite.pl", "Termometry", 20L },
                    { 54L, "second child", 1, "https://www.examplesite.pl", "Produkty konopne", 21L },
                    { 55L, "second child", 2, "https://www.examplesite.pl", "Olejki eteryczne", 21L },
                    { 56L, "second child", 1, "https://www.examplesite.pl", "Felgi aluminiowe", 22L },
                    { 57L, "second child", 2, "https://www.examplesite.pl", "Do motocykli", 22L },
                    { 58L, "second child", 2, "https://www.examplesite.pl", "Układ zawieszenia", 23L },
                    { 59L, "second child", 1, "https://www.examplesite.pl", "Części karoserii", 23L },
                    { 60L, "second child", 2, "https://www.examplesite.pl", "Zestawy narzędzi", 24L },
                    { 61L, "second child", 1, "https://www.examplesite.pl", "Klucze", 24L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ExpirationDate", "Name", "Price" },
                values: new object[,]
                {
                    { 7L, 7L, "Telefony i akcesoria", new DateTime(2024, 5, 7, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5751), "Jakiś tam telefon", 1.2m },
                    { 8L, 8L, "Komputery", new DateTime(2024, 5, 18, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5753), "Jakiś tam komputer", 112.2m },
                    { 9L, 9L, "AGD", new DateTime(2024, 6, 9, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5757), "Jakaś tam lodówka", 112.26m },
                    { 10L, 10L, "Wyposażenie", new DateTime(2025, 4, 9, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5762), "Jakiś tam obraz", 112.22m },
                    { 11L, 11L, "Narzędzia", new DateTime(2024, 5, 28, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5790), "Jakiś tam młotek", 112.24m },
                    { 12L, 12L, "Ogród", new DateTime(2033, 9, 24, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5793), "Jakaś tam doniczka", 1212.2m },
                    { 13L, 13L, "Produkty spożywcze", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5796), "Jakiś tam chleb", 1412.2m },
                    { 14L, 14L, "Artykuły dla zwierząt", new DateTime(2024, 4, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5798), "Jakaś tam karma ogólna", 112.2m },
                    { 15L, 15L, "Utrzymanie czystości", new DateTime(2024, 5, 7, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5815), "Jakaś tam miotła", 234m },
                    { 16L, 16L, "Pielęgnacja", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5819), "Jakiś tam krem", 1412.2m },
                    { 17L, 17L, "Makijaż", new DateTime(2024, 4, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5821), "Jakaś tam podkład", 112.2m },
                    { 18L, 18L, "Perfumy", new DateTime(2024, 5, 7, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5827), "Jakaś tam dezodorant", 234m },
                    { 19L, 19L, "Domowa apteczka", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5830), "Jakiś tam bandarz", 1412.2m },
                    { 20L, 20L, "Urządzenia medyczne", new DateTime(2024, 4, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5833), "Jakiś tam rozrusznik serca", 112.2m },
                    { 21L, 21L, "Medycyna naturalna", new DateTime(2024, 5, 7, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5835), "Jakaś tam herbatka naturalna", 234m },
                    { 22L, 22L, "Opony i felgi", new DateTime(2024, 8, 23, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5856), "Jakieś tam felgi", 1412.2m },
                    { 23L, 23L, "Części samochodowe", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5859), "Jakaś tam lampa", 112.2m },
                    { 24L, 24L, "Warsztat", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5861), "Jakaś tam śrubka", 234m }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DisplayOrder", "ImageURL", "Name", "ParentId" },
                values: new object[,]
                {
                    { 62L, "third child", 1, "https://www.examplesite.pl", "Odkurzacze pionowe", 29L },
                    { 63L, "third child", 2, "https://www.examplesite.pl", "Do kuchni", 29L },
                    { 64L, "third child", 2, "https://www.examplesite.pl", "Płyty grzewcze", 30L },
                    { 65L, "third child", 1, "https://www.examplesite.pl", "Okapy", 30L },
                    { 66L, "third child", 1, "https://www.examplesite.pl", "Lodówki", 31L },
                    { 67L, "third child", 2, "https://www.examplesite.pl", "Pralki", 31L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ExpirationDate", "Name", "Price" },
                values: new object[,]
                {
                    { 25L, 25L, "Smartfony", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5864), "Jakiś tam IPhone", 112.2m },
                    { 26L, 26L, "Tablety", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5867), "Jakiś tam tablet", 234.1m },
                    { 27L, 27L, "Laptopy", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5869), "Jakiś tam laptop acer", 112.2m },
                    { 28L, 28L, "Komputery stacjonarne", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5872), "Jakiś tam stacjonarny", 234.1m },
                    { 29L, 29L, "AGD małe", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5874), "Jakieś tam małe agd", 112.2m },
                    { 30L, 30L, "AGD do zabudowy", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5877), "Jakaś tam agd zabudowa", 234.1m },
                    { 31L, 31L, "AGD", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5879), "Jakiś tam odkurzacz agd", 234.1m },
                    { 32L, 32L, "Garnki i patelnie", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5899), "Jakiś tam garnek", 112.2m },
                    { 33L, 33L, "Dekoracje i ozdoby", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5902), "Jakaś tam dekoracja", 234.1m },
                    { 34L, 34L, "Piły i pilarki", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5908), "Jakaś tam piła", 112.2m },
                    { 35L, 35L, "Odkurzacze przemysłowe", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5911), "Jakiś tam przemysłowy odkurzacz", 234.1m },
                    { 36L, 36L, "Nawozy i preparaty", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5994), "Jakiś tam nawóz", 112.2m },
                    { 37L, 37L, "Meble ogrodowe", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(5998), "Jakiś tam mebel do ogrodu", 234.1m },
                    { 38L, 38L, "Kawy", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6003), "Jakaś tam kawa", 112.2m },
                    { 39L, 39L, "Herbaty", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6005), "Jakaś tam herbata", 234.1m },
                    { 40L, 40L, "Karmy", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6008), "Jakaś tam karma", 112.2m },
                    { 41L, 41L, "Legowiska", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6011), "Jakieś tam legowisko", 234.1m },
                    { 42L, 42L, "Środki czyszczące", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6014), "Jakaś tam szmatka", 112.2m },
                    { 43L, 43L, "Suszarki na pranie", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6034), "Jakaś tam suszarka", 234.1m },
                    { 44L, 44L, "Kremy do twarzy", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6037), "Jakiś tam krem na twarz", 112.2m },
                    { 45L, 45L, "Szampony", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6040), "Jakiś tam szampon", 234.1m },
                    { 46L, 46L, "Makijaż ust", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6042), "Jakaś tam szminka", 112.2m },
                    { 47L, 47L, "Sztuczne rzęsy", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6045), "Jakaś tam rzęsa", 234.1m },
                    { 48L, 48L, "Zestawy", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6048), "Jakiś tam zestaw perfum", 112.2m },
                    { 49L, 49L, "Wody toaletowe", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6050), "Jakaś tam woda", 234.1m },
                    { 50L, 50L, "Suplementy diety", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6053), "Jakiś tam suplement", 112.2m },
                    { 51L, 51L, "Leki bez recepty", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6055), "Jakaś tam tabletka", 234.1m },
                    { 52L, 52L, "Inhalatory", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6058), "Jakiś tam inhalator", 112.2m },
                    { 53L, 53L, "Termometry", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6078), "Jakiś tam termometr", 234.1m },
                    { 54L, 54L, "Produkty konopne", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6081), "Jakaś tam roślinka CBD", 112.2m },
                    { 55L, 55L, "Olejki eteryczne", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6084), "Jakiś tam olejek", 234.1m },
                    { 56L, 56L, "Felgi aluminiowe", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6086), "Jakaś tam felga alu", 112.2m },
                    { 57L, 57L, "Do motocykli", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6089), "Jakieś tam motory", 234.1m },
                    { 58L, 58L, "Układ zawieszenia", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6092), "Jakaś tam sprężyna ", 112.2m },
                    { 59L, 59L, "Części karoserii", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6094), "Jakaś tam maska", 234.1m },
                    { 60L, 60L, "Zestawy narzędzi", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6097), "Jakaś tam paczka narzędzi", 112.2m },
                    { 61L, 61L, "Klucze", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6099), "Jakiś tam klucz", 234.1m },
                    { 62L, 62L, "Odkurzacze pionowe", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6102), "Jakiś tam odkurzacz pionowy 2000", 112.2m },
                    { 63L, 63L, "Do kuchni", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6105), "Jakiś tam zlew", 234.1m },
                    { 64L, 64L, "Płyty grzewcze", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6108), "Jakaś tam grzałka", 112.2m },
                    { 65L, 65L, "Okapy", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6111), "Jakiś tam okap", 234.1m },
                    { 66L, 66L, "Lodówki", new DateTime(2024, 8, 15, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6118), "Jakaś tam lodówka ", 112.2m },
                    { 67L, 67L, "Pralki", new DateTime(2024, 4, 25, 16, 31, 16, 189, DateTimeKind.Utc).AddTicks(6120), "Jakaś tam pralka", 234.1m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentId",
                table: "Categories",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
