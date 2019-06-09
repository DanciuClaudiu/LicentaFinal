using Microsoft.EntityFrameworkCore.Migrations;

namespace LicentaFinal.Data.Migrations
{
    public partial class w : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartIntrumentQuantity",
                table: "Cart",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CartIntrumentQuantity",
                table: "Cart");
        }
    }
}
