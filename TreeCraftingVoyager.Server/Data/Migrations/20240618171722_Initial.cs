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
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1L, 1L, "Elektronika", new DateTime(2027, 3, 15, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(6315), "Coś elektronicznego", 1.2m },
                    { 2L, 2L, "Dom i ogród", new DateTime(2024, 9, 26, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7298), "Coś do domu i ogrodu", 20.3m },
                    { 3L, 3L, "Super market", new DateTime(2024, 6, 20, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7302), "Coś z super marketu", 2.2m },
                    { 4L, 4L, "Uroda", new DateTime(2024, 7, 13, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7304), "Coś dla urody", 30.2m },
                    { 5L, 5L, "Zdrowie", new DateTime(2024, 7, 18, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7307), "Coś dla zdrowia", 11.2m },
                    { 6L, 6L, "Motoryzacja", new DateTime(2025, 4, 14, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7319), "Coś dla motoryzacji", 1123.2m }
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
                    { 7L, 7L, "Telefony i akcesoria", new DateTime(2024, 7, 11, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7345), "Jakiś tam telefon", 1.2m },
                    { 8L, 8L, "Komputery", new DateTime(2024, 7, 22, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7347), "Jakiś tam komputer", 112.2m },
                    { 9L, 9L, "AGD", new DateTime(2024, 8, 13, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7497), "Jakaś tam lodówka", 112.26m },
                    { 10L, 10L, "Wyposażenie", new DateTime(2025, 6, 13, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7504), "Jakiś tam obraz", 112.22m },
                    { 11L, 11L, "Narzędzia", new DateTime(2024, 8, 1, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7507), "Jakiś tam młotek", 112.24m },
                    { 12L, 12L, "Ogród", new DateTime(2033, 11, 28, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7509), "Jakaś tam doniczka", 1212.2m },
                    { 13L, 13L, "Produkty spożywcze", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7511), "Jakiś tam chleb", 1412.2m },
                    { 14L, 14L, "Artykuły dla zwierząt", new DateTime(2024, 6, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7514), "Jakaś tam karma ogólna", 112.2m },
                    { 15L, 15L, "Utrzymanie czystości", new DateTime(2024, 7, 11, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7531), "Jakaś tam miotła", 234m },
                    { 16L, 16L, "Pielęgnacja", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7534), "Jakiś tam krem", 1412.2m },
                    { 17L, 17L, "Makijaż", new DateTime(2024, 6, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7557), "Jakaś tam podkład", 112.2m },
                    { 18L, 18L, "Perfumy", new DateTime(2024, 7, 11, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7564), "Jakaś tam dezodorant", 234m },
                    { 19L, 19L, "Domowa apteczka", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7567), "Jakiś tam bandarz", 1412.2m },
                    { 20L, 20L, "Urządzenia medyczne", new DateTime(2024, 6, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7569), "Jakiś tam rozrusznik serca", 112.2m },
                    { 21L, 21L, "Medycyna naturalna", new DateTime(2024, 7, 11, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7571), "Jakaś tam herbatka naturalna", 234m },
                    { 22L, 22L, "Opony i felgi", new DateTime(2024, 10, 27, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7574), "Jakieś tam felgi", 1412.2m },
                    { 23L, 23L, "Części samochodowe", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7576), "Jakaś tam lampa", 112.2m },
                    { 24L, 24L, "Warsztat", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7578), "Jakaś tam śrubka", 234m }
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
                    { 25L, 25L, "Smartfony", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7580), "Jakiś tam IPhone", 112.2m },
                    { 26L, 26L, "Tablety", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7583), "Jakiś tam tablet", 234.1m },
                    { 27L, 27L, "Laptopy", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7585), "Jakiś tam laptop acer", 112.2m },
                    { 28L, 28L, "Komputery stacjonarne", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7604), "Jakiś tam stacjonarny", 234.1m },
                    { 29L, 29L, "AGD małe", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7607), "Jakieś tam małe agd", 112.2m },
                    { 30L, 30L, "AGD do zabudowy", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7609), "Jakaś tam agd zabudowa", 234.1m },
                    { 31L, 31L, "AGD", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7611), "Jakiś tam odkurzacz agd", 234.1m },
                    { 32L, 32L, "Garnki i patelnie", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7613), "Jakiś tam garnek", 112.2m },
                    { 33L, 33L, "Dekoracje i ozdoby", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7616), "Jakaś tam dekoracja", 234.1m },
                    { 34L, 34L, "Piły i pilarki", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7622), "Jakaś tam piła", 112.2m },
                    { 35L, 35L, "Odkurzacze przemysłowe", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7624), "Jakiś tam przemysłowy odkurzacz", 234.1m },
                    { 36L, 36L, "Nawozy i preparaty", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7627), "Jakiś tam nawóz", 112.2m },
                    { 37L, 37L, "Meble ogrodowe", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7629), "Jakiś tam mebel do ogrodu", 234.1m },
                    { 38L, 38L, "Kawy", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7648), "Jakaś tam kawa", 112.2m },
                    { 39L, 39L, "Herbaty", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7651), "Jakaś tam herbata", 234.1m },
                    { 40L, 40L, "Karmy", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7653), "Jakaś tam karma", 112.2m },
                    { 41L, 41L, "Legowiska", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7656), "Jakieś tam legowisko", 234.1m },
                    { 42L, 42L, "Środki czyszczące", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7658), "Jakaś tam szmatka", 112.2m },
                    { 43L, 43L, "Suszarki na pranie", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7660), "Jakaś tam suszarka", 234.1m },
                    { 44L, 44L, "Kremy do twarzy", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7662), "Jakiś tam krem na twarz", 112.2m },
                    { 45L, 45L, "Szampony", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7667), "Jakiś tam szampon", 234.1m },
                    { 46L, 46L, "Makijaż ust", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7669), "Jakaś tam szminka", 112.2m },
                    { 47L, 47L, "Sztuczne rzęsy", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7672), "Jakaś tam rzęsa", 234.1m },
                    { 48L, 48L, "Zestawy", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7674), "Jakiś tam zestaw perfum", 112.2m },
                    { 49L, 49L, "Wody toaletowe", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7692), "Jakaś tam woda", 234.1m },
                    { 50L, 50L, "Suplementy diety", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7695), "Jakiś tam suplement", 112.2m },
                    { 51L, 51L, "Leki bez recepty", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7697), "Jakaś tam tabletka", 234.1m },
                    { 52L, 52L, "Inhalatory", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7700), "Jakiś tam inhalator", 112.2m },
                    { 53L, 53L, "Termometry", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7703), "Jakiś tam termometr", 234.1m },
                    { 54L, 54L, "Produkty konopne", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7705), "Jakaś tam roślinka CBD", 112.2m },
                    { 55L, 55L, "Olejki eteryczne", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7708), "Jakiś tam olejek", 234.1m },
                    { 56L, 56L, "Felgi aluminiowe", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7710), "Jakaś tam felga alu", 112.2m },
                    { 57L, 57L, "Do motocykli", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7712), "Jakieś tam motory", 234.1m },
                    { 58L, 58L, "Układ zawieszenia", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7715), "Jakaś tam sprężyna ", 112.2m },
                    { 59L, 59L, "Części karoserii", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7717), "Jakaś tam maska", 234.1m },
                    { 60L, 60L, "Zestawy narzędzi", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7720), "Jakaś tam paczka narzędzi", 112.2m },
                    { 61L, 61L, "Klucze", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7722), "Jakiś tam klucz", 234.1m },
                    { 62L, 62L, "Odkurzacze pionowe", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7724), "Jakiś tam odkurzacz pionowy 2000", 112.2m },
                    { 63L, 63L, "Do kuchni", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7727), "Jakiś tam zlew", 234.1m },
                    { 64L, 64L, "Płyty grzewcze", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7729), "Jakaś tam grzałka", 112.2m },
                    { 65L, 65L, "Okapy", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7731), "Jakiś tam okap", 234.1m },
                    { 66L, 66L, "Lodówki", new DateTime(2024, 10, 19, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7738), "Jakaś tam lodówka ", 112.2m },
                    { 67L, 67L, "Pralki", new DateTime(2024, 6, 29, 17, 17, 20, 537, DateTimeKind.Utc).AddTicks(7740), "Jakaś tam pralka", 234.1m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

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
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
