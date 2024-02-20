using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPIDemo.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarSeat = table.Column<int>(type: "int", nullable: true),
                    CarType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CarPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "CarId", "CarBrand", "CarColor", "CarName", "CarPrice", "CarSeat", "CarType" },
                values: new object[,]
                {
                    { 1, "Toyota", "Silver", "Camry", 25000.0, 4, "Sedan" },
                    { 2, "Honda", "Red", "Civic", 23000.0, 4, "Sedan" },
                    { 3, "Ford", "Blue", "F-150", 35000.0, 2, "Sport" },
                    { 4, "Chevrolet", "Black", "Camaro", 45000.0, 2, "Sport" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
