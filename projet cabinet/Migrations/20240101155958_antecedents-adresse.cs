using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet_cabinet.Migrations
{
    public partial class antecedentsadresse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Antecedents",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Antecedents",
                table: "Users");
        }
    }
}
