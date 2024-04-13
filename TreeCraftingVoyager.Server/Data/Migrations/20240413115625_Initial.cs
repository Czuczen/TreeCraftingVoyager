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
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageURL = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                    { 1L, "", 0, "", "Elektronika", null },
                    { 2L, "", 0, "", "Dom i ogród", null },
                    { 3L, "", 0, "", "Super market", null },
                    { 4L, "", 0, "", "Uroda", null },
                    { 5L, "", 0, "", "Zdrowie", null },
                    { 6L, "", 0, "", "Motoryzacja", null },
                    { 7L, "", 0, "", "Telefony i akcesoria", 1L },
                    { 8L, "", 0, "", "Komputery", 1L },
                    { 9L, "", 0, "", "AGD", 1L },
                    { 10L, "", 0, "", "Wyposażenie", 2L },
                    { 11L, "", 0, "", "Narzędzia", 2L },
                    { 12L, "", 0, "", "Ogród", 2L },
                    { 13L, "", 0, "", "Produkty spożywcze", 3L },
                    { 14L, "", 0, "", "Artykuły dla zwierząt", 3L },
                    { 15L, "", 0, "", "Utrzymanie czystości", 3L },
                    { 16L, "", 0, "", "Pielęgnacja", 4L },
                    { 17L, "", 0, "", "Makijaż", 4L },
                    { 18L, "", 0, "", "Perfumy", 4L },
                    { 19L, "", 0, "", "Domowa apteczka", 5L },
                    { 20L, "", 0, "", "Urządzenia medyczne", 5L },
                    { 21L, "", 0, "", "Medycyna naturalna", 5L },
                    { 22L, "", 0, "", "Opony i felgi", 6L },
                    { 23L, "", 0, "", "Części samochodowe", 6L },
                    { 24L, "", 0, "", "Warsztat", 6L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ExpirationDate", "Name", "Price" },
                values: new object[,]
                {
                    { 1L, 1L, "Jakiś tam produkt elektroniczny", new DateTime(2027, 1, 8, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(3456), "Coś elektronicznego", 1.2m },
                    { 2L, 2L, "Jakiś tam produkt do domu i ogrodu", new DateTime(2024, 7, 22, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4549), "Coś do domu i ogrodu", 20.3m },
                    { 3L, 3L, "Jakiś tam produkt z super marketu", new DateTime(2024, 4, 15, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4554), "Coś z super marketu", 2.2m },
                    { 4L, 4L, "Jakiś tam produkt dla urody", new DateTime(2024, 5, 8, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4583), "Coś dla urody", 30.2m },
                    { 5L, 5L, "Jakiś tam produkt dla zdrowia", new DateTime(2024, 5, 13, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4592), "Coś dla zdrowia", 11.2m },
                    { 6L, 6L, "Jakiś tam produkt dla motoryzacji", new DateTime(2025, 2, 7, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4608), "Coś dla motoryzacji", 1123.2m }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DisplayOrder", "ImageURL", "Name", "ParentId" },
                values: new object[,]
                {
                    { 25L, "", 0, "", "Smartfony", 7L },
                    { 26L, "", 0, "", "Tablety", 7L },
                    { 27L, "", 0, "", "Laptopy", 8L },
                    { 28L, "", 0, "", "Komputery stacjonarne", 8L },
                    { 29L, "", 0, "", "AGD małe", 9L },
                    { 30L, "", 0, "", "AGD do zabudowy", 9L },
                    { 31L, "", 0, "", "AGD", 9L },
                    { 32L, "", 0, "", "Garnki i patelnie", 10L },
                    { 33L, "", 0, "", "Dekoracje i ozdoby", 10L },
                    { 34L, "", 0, "", "Piły i pilarki", 11L },
                    { 35L, "", 0, "", "Odkurzacze przemysłowe", 11L },
                    { 36L, "", 0, "", "Nawozy i preparaty", 12L },
                    { 37L, "", 0, "", "Meble ogrodowe", 12L },
                    { 38L, "", 0, "", "Kawy", 13L },
                    { 39L, "", 0, "", "Herbaty", 13L },
                    { 40L, "", 0, "", "Karmy", 14L },
                    { 41L, "", 0, "", "Legowiska", 14L },
                    { 42L, "", 0, "", "Środki czyszczące", 15L },
                    { 43L, "", 0, "", "Suszarki na pranie", 15L },
                    { 44L, "", 0, "", "Kremy do twarzy", 16L },
                    { 45L, "", 0, "", "Szampony", 16L },
                    { 46L, "", 0, "", "Makijaż ust", 17L },
                    { 47L, "", 0, "", "Sztuczne rzęsy", 17L },
                    { 48L, "", 0, "", "Zestawy", 18L },
                    { 49L, "", 0, "", "Wody toaletowe", 18L },
                    { 50L, "", 0, "", "Suplementy diety", 19L },
                    { 51L, "", 0, "", "Leki bez recepty", 19L },
                    { 52L, "", 0, "", "Inhalatory", 20L },
                    { 53L, "", 0, "", "Termometry", 20L },
                    { 54L, "", 0, "", "Produkty konopne", 21L },
                    { 55L, "", 0, "", "Olejki eteryczne", 21L },
                    { 56L, "", 0, "", "Felgi aluminiowe", 22L },
                    { 57L, "", 0, "", "Do motocykli", 22L },
                    { 58L, "", 0, "", "Układ zawieszenia", 23L },
                    { 59L, "", 0, "", "Części karoserii", 23L },
                    { 60L, "", 0, "", "Zestawy narzędzi", 24L },
                    { 61L, "", 0, "", "Klucze", 24L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ExpirationDate", "Name", "Price" },
                values: new object[,]
                {
                    { 7L, 7L, "Telefony i akcesoria", new DateTime(2024, 5, 6, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4614), "Jakiś tam telefon", 1.2m },
                    { 8L, 8L, "Komputery", new DateTime(2024, 5, 17, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4617), "Jakiś tam komputer", 112.2m },
                    { 9L, 9L, "AGD", new DateTime(2024, 6, 8, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4619), "Jakaś tam lodówka", 112.26m },
                    { 10L, 10L, "Wyposażenie", new DateTime(2025, 4, 8, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4751), "Jakiś tam obraz", 112.22m },
                    { 11L, 11L, "Narzędzia", new DateTime(2024, 5, 27, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4756), "Jakiś tam młotek", 112.24m },
                    { 12L, 12L, "Ogród", new DateTime(2033, 9, 23, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4758), "Jakaś tam doniczka", 1212.2m },
                    { 13L, 13L, "Produkty spożywcze", new DateTime(2024, 4, 24, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4761), "Jakiś tam chleb", 1412.2m },
                    { 14L, 14L, "Artykuły dla zwierząt", new DateTime(2024, 4, 14, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4768), "Jakaś tam karma ogólna", 112.2m },
                    { 15L, 15L, "Utrzymanie czystości", new DateTime(2024, 5, 6, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4794), "Jakaś tam miotła", 234m },
                    { 16L, 16L, "Pielęgnacja", new DateTime(2024, 4, 24, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4800), "Jakiś tam krem", 1412.2m },
                    { 17L, 17L, "Makijaż", new DateTime(2024, 4, 14, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4803), "Jakaś tam podkład", 112.2m },
                    { 18L, 18L, "Perfumy", new DateTime(2024, 5, 6, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4811), "Jakaś tam dezodorant", 234m },
                    { 19L, 19L, "Domowa apteczka", new DateTime(2024, 4, 24, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4816), "Jakiś tam bandarz", 1412.2m },
                    { 20L, 20L, "Urządzenia medyczne", new DateTime(2024, 4, 14, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4818), "Jakiś tam rozrusznik serca", 112.2m },
                    { 21L, 21L, "Medycyna naturalna", new DateTime(2024, 5, 6, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4821), "Jakaś tam herbatka naturalna", 234m },
                    { 22L, 22L, "Opony i felgi", new DateTime(2024, 8, 22, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4823), "Jakieś tam felgi", 1412.2m },
                    { 23L, 23L, "Części samochodowe", new DateTime(2024, 8, 14, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4825), "Jakaś tam lampa", 112.2m },
                    { 24L, 24L, "Warsztat", new DateTime(2024, 4, 24, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4827), "Jakaś tam śrubka", 234m }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DisplayOrder", "ImageURL", "Name", "ParentId" },
                values: new object[,]
                {
                    { 62L, "", 0, "", "Odkurzacze pionowe", 29L },
                    { 63L, "", 0, "", "Do kuchni", 29L },
                    { 64L, "", 0, "", "Płyty grzewcze", 30L },
                    { 65L, "", 0, "", "Okapy", 30L },
                    { 66L, "", 0, "", "Lodówki", 31L },
                    { 67L, "", 0, "", "Pralki", 31L }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ExpirationDate", "Name", "Price" },
                values: new object[,]
                {
                    { 25L, 25L, "", new DateTime(2024, 8, 14, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4830), "Jakaś tam ", 112.2m },
                    { 26L, 26L, "", new DateTime(2024, 4, 24, 11, 56, 24, 63, DateTimeKind.Utc).AddTicks(4906), "Jakaś tam ", 234.1m }
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
