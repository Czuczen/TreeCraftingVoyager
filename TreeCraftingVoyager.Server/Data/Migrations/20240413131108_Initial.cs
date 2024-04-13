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
                    { 1L, 1L, "Elektronika", new DateTime(2027, 1, 8, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8069), "Coś elektronicznego", 1.2m },
                    { 2L, 2L, "Dom i ogród", new DateTime(2024, 7, 22, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8853), "Coś do domu i ogrodu", 20.3m },
                    { 3L, 3L, "Super market", new DateTime(2024, 4, 15, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8857), "Coś z super marketu", 2.2m },
                    { 4L, 4L, "Uroda", new DateTime(2024, 5, 8, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8860), "Coś dla urody", 30.2m },
                    { 5L, 5L, "Zdrowie", new DateTime(2024, 5, 13, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8890), "Coś dla zdrowia", 11.2m },
                    { 6L, 6L, "Motoryzacja", new DateTime(2025, 2, 7, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8900), "Coś dla motoryzacji", 1123.2m }
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
                    { 7L, 7L, "Telefony i akcesoria", new DateTime(2024, 5, 6, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8904), "Jakiś tam telefon", 1.2m },
                    { 8L, 8L, "Komputery", new DateTime(2024, 5, 17, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8906), "Jakiś tam komputer", 112.2m },
                    { 9L, 9L, "AGD", new DateTime(2024, 6, 8, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8909), "Jakaś tam lodówka", 112.26m },
                    { 10L, 10L, "Wyposażenie", new DateTime(2025, 4, 8, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8914), "Jakiś tam obraz", 112.22m },
                    { 11L, 11L, "Narzędzia", new DateTime(2024, 5, 27, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8917), "Jakiś tam młotek", 112.24m },
                    { 12L, 12L, "Ogród", new DateTime(2033, 9, 23, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8919), "Jakaś tam doniczka", 1212.2m },
                    { 13L, 13L, "Produkty spożywcze", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8922), "Jakiś tam chleb", 1412.2m },
                    { 14L, 14L, "Artykuły dla zwierząt", new DateTime(2024, 4, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8924), "Jakaś tam karma ogólna", 112.2m },
                    { 15L, 15L, "Utrzymanie czystości", new DateTime(2024, 5, 6, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8942), "Jakaś tam miotła", 234m },
                    { 16L, 16L, "Pielęgnacja", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8945), "Jakiś tam krem", 1412.2m },
                    { 17L, 17L, "Makijaż", new DateTime(2024, 4, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8947), "Jakaś tam podkład", 112.2m },
                    { 18L, 18L, "Perfumy", new DateTime(2024, 5, 6, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8971), "Jakaś tam dezodorant", 234m },
                    { 19L, 19L, "Domowa apteczka", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8974), "Jakiś tam bandarz", 1412.2m },
                    { 20L, 20L, "Urządzenia medyczne", new DateTime(2024, 4, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8976), "Jakiś tam rozrusznik serca", 112.2m },
                    { 21L, 21L, "Medycyna naturalna", new DateTime(2024, 5, 6, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8979), "Jakaś tam herbatka naturalna", 234m },
                    { 22L, 22L, "Opony i felgi", new DateTime(2024, 8, 22, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8983), "Jakieś tam felgi", 1412.2m },
                    { 23L, 23L, "Części samochodowe", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8986), "Jakaś tam lampa", 112.2m },
                    { 24L, 24L, "Warsztat", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8989), "Jakaś tam śrubka", 234m }
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
                    { 25L, 25L, "Smartfony", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8992), "Jakiś tam IPhone", 112.2m },
                    { 26L, 26L, "Tablety", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8995), "Jakiś tam tablet", 234.1m },
                    { 27L, 27L, "Laptopy", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(8997), "Jakiś tam laptop acer", 112.2m },
                    { 28L, 28L, "Komputery stacjonarne", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9000), "Jakiś tam stacjonarny", 234.1m },
                    { 29L, 29L, "AGD małe", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9003), "Jakieś tam małe agd", 112.2m },
                    { 30L, 30L, "AGD do zabudowy", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9023), "Jakaś tam agd zabudowa", 234.1m },
                    { 31L, 31L, "AGD", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9026), "Jakiś tam odkurzacz agd", 234.1m },
                    { 32L, 32L, "Garnki i patelnie", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9029), "Jakiś tam garnek", 112.2m },
                    { 33L, 33L, "Dekoracje i ozdoby", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9032), "Jakaś tam dekoracja", 234.1m },
                    { 34L, 34L, "Piły i pilarki", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9037), "Jakaś tam piła", 112.2m },
                    { 35L, 35L, "Odkurzacze przemysłowe", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9040), "Jakiś tam przemysłowy odkurzacz", 234.1m },
                    { 36L, 36L, "Nawozy i preparaty", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9043), "Jakiś tam nawóz", 112.2m },
                    { 37L, 37L, "Meble ogrodowe", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9046), "Jakiś tam mebel do ogrodu", 234.1m },
                    { 38L, 38L, "Kawy", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9049), "Jakaś tam kawa", 112.2m },
                    { 39L, 39L, "Herbaty", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9051), "Jakaś tam herbata", 234.1m },
                    { 40L, 40L, "Karmy", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9054), "Jakaś tam karma", 112.2m },
                    { 41L, 41L, "Legowiska", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9057), "Jakieś tam legowisko", 234.1m },
                    { 42L, 42L, "Środki czyszczące", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9060), "Jakaś tam szmatka", 112.2m },
                    { 43L, 43L, "Suszarki na pranie", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9080), "Jakaś tam suszarka", 234.1m },
                    { 44L, 44L, "Kremy do twarzy", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9084), "Jakiś tam krem na twarz", 112.2m },
                    { 45L, 45L, "Szampony", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9086), "Jakiś tam szampon", 234.1m },
                    { 46L, 46L, "Makijaż ust", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9089), "Jakaś tam szminka", 112.2m },
                    { 47L, 47L, "Sztuczne rzęsy", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9092), "Jakaś tam rzęsa", 234.1m },
                    { 48L, 48L, "Zestawy", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9095), "Jakiś tam zestaw perfum", 112.2m },
                    { 49L, 49L, "Wody toaletowe", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9097), "Jakaś tam woda", 234.1m },
                    { 50L, 50L, "Suplementy diety", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9100), "Jakiś tam suplement", 112.2m },
                    { 51L, 51L, "Leki bez recepty", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9103), "Jakaś tam tabletka", 234.1m },
                    { 52L, 52L, "Inhalatory", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9106), "Jakiś tam inhalator", 112.2m },
                    { 53L, 53L, "Termometry", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9109), "Jakiś tam termometr", 234.1m },
                    { 54L, 54L, "Produkty konopne", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9112), "Jakaś tam roślinka CBD", 112.2m },
                    { 55L, 55L, "Olejki eteryczne", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9131), "Jakiś tam olejek", 234.1m },
                    { 56L, 56L, "Felgi aluminiowe", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9135), "Jakaś tam felga alu", 112.2m },
                    { 57L, 57L, "Do motocykli", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9138), "Jakieś tam motory", 234.1m },
                    { 58L, 58L, "Układ zawieszenia", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9141), "Jakaś tam sprężyna ", 112.2m },
                    { 59L, 59L, "Części karoserii", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9143), "Jakaś tam maska", 234.1m },
                    { 60L, 60L, "Zestawy narzędzi", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9146), "Jakaś tam paczka narzędzi", 112.2m },
                    { 61L, 61L, "Klucze", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9149), "Jakiś tam klucz", 234.1m },
                    { 62L, 62L, "Odkurzacze pionowe", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9152), "Jakiś tam odkurzacz pionowy 2000", 112.2m },
                    { 63L, 63L, "Do kuchni", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9154), "Jakiś tam zlew", 234.1m },
                    { 64L, 64L, "Płyty grzewcze", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9157), "Jakaś tam grzałka", 112.2m },
                    { 65L, 65L, "Okapy", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9160), "Jakiś tam okap", 234.1m },
                    { 66L, 66L, "Lodówki", new DateTime(2024, 8, 14, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9356), "Jakaś tam lodówka ", 112.2m },
                    { 67L, 67L, "Pralki", new DateTime(2024, 4, 24, 13, 11, 5, 998, DateTimeKind.Utc).AddTicks(9361), "Jakaś tam pralka", 234.1m }
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
