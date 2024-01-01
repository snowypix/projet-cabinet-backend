using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projet_cabinet.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DossierID = table.Column<int>(type: "int", nullable: true),
                    PatientID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dossiers",
                columns: table => new
                {
                    DossierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    MedecinID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossiers", x => x.DossierID);
                    table.ForeignKey(
                        name: "FK_Dossiers_Users_MedecinID",
                        column: x => x.MedecinID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dossiers_Users_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RDVs",
                columns: table => new
                {
                    RDVId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Heure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedecinID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RDVs", x => x.RDVId);
                    table.ForeignKey(
                        name: "FK_RDVs_Users_MedecinID",
                        column: x => x.MedecinID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RDVs_Users_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examens",
                columns: table => new
                {
                    ExamenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamenDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExamenNom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resultat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DossierID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examens", x => x.ExamenID);
                    table.ForeignKey(
                        name: "FK_Examens_Dossiers_DossierID",
                        column: x => x.DossierID,
                        principalTable: "Dossiers",
                        principalColumn: "DossierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicaments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DossierID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Dossiers_DossierID",
                        column: x => x.DossierID,
                        principalTable: "Dossiers",
                        principalColumn: "DossierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_MedecinID",
                table: "Dossiers",
                column: "MedecinID");

            migrationBuilder.CreateIndex(
                name: "IX_Dossiers_PatientID",
                table: "Dossiers",
                column: "PatientID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examens_DossierID",
                table: "Examens",
                column: "DossierID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DossierID",
                table: "Prescriptions",
                column: "DossierID");

            migrationBuilder.CreateIndex(
                name: "IX_RDVs_MedecinID",
                table: "RDVs",
                column: "MedecinID");

            migrationBuilder.CreateIndex(
                name: "IX_RDVs_PatientID",
                table: "RDVs",
                column: "PatientID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examens");

            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "RDVs");

            migrationBuilder.DropTable(
                name: "Dossiers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
