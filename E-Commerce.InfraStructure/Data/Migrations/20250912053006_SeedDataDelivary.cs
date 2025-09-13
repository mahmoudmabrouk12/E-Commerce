using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.InfraStructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataDelivary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DelivaryMethods",
                columns: new[] { "Id", "DelivaryTime", "Description", "Name", "Price" },
                values: new object[] { 1, "5-7 days", "Delivers in 5-7 business days", "Standard Delivery", 50m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DelivaryMethods",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
