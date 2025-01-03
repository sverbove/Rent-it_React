using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent_it.React.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVerhuurModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerhuurAanvragen_Klant_KlantID",
                table: "VerhuurAanvragen");

            migrationBuilder.DropTable(
                name: "Klant");

            migrationBuilder.RenameColumn(
                name: "KlantID",
                table: "VerhuurAanvragen",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_VerhuurAanvragen_KlantID",
                table: "VerhuurAanvragen",
                newName: "IX_VerhuurAanvragen_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_VerhuurAanvragen_Accounts_AccountId",
                table: "VerhuurAanvragen",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VerhuurAanvragen_Accounts_AccountId",
                table: "VerhuurAanvragen");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "VerhuurAanvragen",
                newName: "KlantID");

            migrationBuilder.RenameIndex(
                name: "IX_VerhuurAanvragen_AccountId",
                table: "VerhuurAanvragen",
                newName: "IX_VerhuurAanvragen_KlantID");

            migrationBuilder.CreateTable(
                name: "Klant",
                columns: table => new
                {
                    KlantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RijbewijsDocNr = table.Column<int>(type: "int", nullable: false),
                    TelNr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klant", x => x.KlantID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_VerhuurAanvragen_Klant_KlantID",
                table: "VerhuurAanvragen",
                column: "KlantID",
                principalTable: "Klant",
                principalColumn: "KlantID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
