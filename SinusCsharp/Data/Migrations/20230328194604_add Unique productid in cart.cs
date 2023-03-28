using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SinusCsharp.Data.Migrations
{
    /// <inheritdoc />
    public partial class addUniqueproductidincart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cart_ProductId",
                table: "Cart",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cart_ProductId",
                table: "Cart");
        }
    }
}
