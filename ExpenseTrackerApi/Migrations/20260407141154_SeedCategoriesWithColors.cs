using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedCategoriesWithColors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "HexColor", "Icon", "Name" },
                values: new object[,]
                {
                    { 1, "#FF9800", "default_icon", "Alimentação" },
                    { 2, "#2196F3", "default_icon", "Compras" },
                    { 3, "#4CAF50", "default_icon", "Transporte" },
                    { 4, "#9C27B0", "default_icon", "Lazer" },
                    { 5, "#F44336", "default_icon", "Saúde" },
                    { 6, "#3F51B5", "default_icon", "Educação" },
                    { 7, "#795548", "default_icon", "Moradia" },
                    { 8, "#607D8B", "default_icon", "Outros" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
