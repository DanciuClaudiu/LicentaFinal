using Microsoft.EntityFrameworkCore.Migrations;

namespace LicentaFinal.Data.Migrations
{
    public partial class h : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Instrument",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImageUrl",
                table: "Instrument",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
