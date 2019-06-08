using Microsoft.EntityFrameworkCore.Migrations;

namespace LicentaFinal.Data.Migrations
{
    public partial class y : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Cart",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Cart",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
