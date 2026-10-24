using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlorMoon.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName" },
                values: new object[,]
                {
                    { 1, "roses" },
                    { 2, "tulips" },
                    { 3, "peonies" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "ImagePath", "Price", "ProductDescription", "ProductName" },
                values: new object[,]
                {
                    { 1, 1, "pinkWhiteRoses.png", 50f, "A beautiful mix of pink and white roses.", "Pink and White Roses Bouquet" },
                    { 2, 1, "whiteRoses.png", 70f, "Elegant all-white rose bouquet.", "White Roses Bouquet" },
                    { 3, 3, "pinkPeony.png", 60f, "Sweet pink peonies.", "Pink Peony Bouquet" },
                    { 4, 2, "pinkTulips.png", 55f, "Fresh spring tulips.", "Pink Tulips Bouquet" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");
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
