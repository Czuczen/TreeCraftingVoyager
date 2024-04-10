using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TreeCraftingVoyager.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class seddData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "DisplayOrder", "ImageURL", "IsActive", "Name", "ParentId", "SEOKeywords" },
                values: new object[,]
                {
                    { 1L, "", 0, "", false, "Elektronika", null, "" },
                    { 2L, "", 0, "", false, "Ogród", null, "" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
