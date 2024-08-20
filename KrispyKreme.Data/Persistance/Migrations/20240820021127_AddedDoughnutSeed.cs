using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KrispyKreme.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDoughnutSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Doughnuts",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Original Glazed Doughnut" },
                    { 2, "Chocolate Iced Glazed Doughnut" },
                    { 3, "Glazed Raspberry Filled Doughnut" },
                    { 4, "Glazed Lemon Filled Doughnut" },
                    { 5, "Pumpkin Spice Cake Doughnut" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doughnuts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Doughnuts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Doughnuts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Doughnuts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Doughnuts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");
        }
    }
}
