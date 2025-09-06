using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace E_Commerce.InfraStructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataIntables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Electronic devices and gadgets", "Electronics" },
                    { 2, "Vehicles and automobiles", "Cars" },
                    { 3, "Fashion and apparel", "Clothing" }
                });

            migrationBuilder.InsertData(
                table: "products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Latest Apple smartphone with A17 chip", "iPhone 15", 1200m },
                    { 2, 1, "Smart 4K Ultra HD LED TV", "Samsung TV 55\"", 800m },
                    { 3, 2, "Reliable sedan with advanced safety features", "Toyota Corolla", 20000m },
                    { 4, 1, "Stylish black leather jacket", "Leather Jacket", 150m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
