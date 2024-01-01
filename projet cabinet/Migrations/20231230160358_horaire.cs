using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet_cabinet.Migrations
{
    public partial class horaire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HoraireDebut",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoraireFin",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoraireDebut",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "HoraireFin",
                table: "Users");
        }
    }
}
