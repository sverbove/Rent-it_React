using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent_it.React.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gebruikersnaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wachtwoord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActief = table.Column<bool>(type: "bit", nullable: false),
                    ParentAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Accounts_Accounts_ParentAccountId",
                        column: x => x.ParentAccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountID");
                });

            migrationBuilder.CreateTable(
                name: "Klant",
                columns: table => new
                {
                    KlantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelNr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RijbewijsDocNr = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klant", x => x.KlantID);
                });

            migrationBuilder.CreateTable(
                name: "Voertuigen",
                columns: table => new
                {
                    VoertuigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Soort = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Merk = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Kleur = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Kenteken = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Aanschafjaar = table.Column<int>(type: "int", nullable: false),
                    PrijsPerDag = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Beschikbaar = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voertuigen", x => x.VoertuigId);
                });

            migrationBuilder.CreateTable(
                name: "Abonnementen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bedrijfsnaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KvkNummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abonnementsvorm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Startdatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnementen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Abonnementen_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerhuurAanvragen",
                columns: table => new
                {
                    VerhuurID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KlantID = table.Column<int>(type: "int", nullable: false),
                    VoertuigID = table.Column<int>(type: "int", nullable: false),
                    VerwachteKilometers = table.Column<int>(type: "int", nullable: false),
                    RijbewijsDocNr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EindDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AardeVanReis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VersteBestemming = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerhuurAanvragen", x => x.VerhuurID);
                    table.ForeignKey(
                        name: "FK_VerhuurAanvragen_Klant_KlantID",
                        column: x => x.KlantID,
                        principalTable: "Klant",
                        principalColumn: "KlantID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VerhuurAanvragen_Voertuigen_VoertuigID",
                        column: x => x.VoertuigID,
                        principalTable: "Voertuigen",
                        principalColumn: "VoertuigId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Abonnementen_AccountID",
                table: "Abonnementen",
                column: "AccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentAccountId",
                table: "Accounts",
                column: "ParentAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VerhuurAanvragen_KlantID",
                table: "VerhuurAanvragen",
                column: "KlantID");

            migrationBuilder.CreateIndex(
                name: "IX_VerhuurAanvragen_VoertuigID",
                table: "VerhuurAanvragen",
                column: "VoertuigID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonnementen");

            migrationBuilder.DropTable(
                name: "VerhuurAanvragen");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Klant");

            migrationBuilder.DropTable(
                name: "Voertuigen");
        }
    }
}
