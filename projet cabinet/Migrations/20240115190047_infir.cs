using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet_cabinet.Migrations
{
    public partial class infir : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Infirmier_HoraireDebut",
                table: "Users",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Infirmier_HoraireFin",
                table: "Users",
                type: "time",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Infirmier_HoraireDebut",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Infirmier_HoraireFin",
                table: "Users");
        }
    }
}
