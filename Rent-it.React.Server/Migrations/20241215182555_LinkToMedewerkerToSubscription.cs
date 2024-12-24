using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent_it.React.Server.Migrations
{
    /// <inheritdoc />
    public partial class LinkToMedewerkerToSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AbonnementDate",
                table: "Abonnementen",
                newName: "Startdatum");

            migrationBuilder.AddColumn<int>(
                name: "AccountID",
                table: "Abonnementen",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Abonnementen_AccountID",
                table: "Abonnementen",
                column: "AccountID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Abonnementen_Accounts_AccountID",
                table: "Abonnementen",
                column: "AccountID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abonnementen_Accounts_AccountID",
                table: "Abonnementen");

            migrationBuilder.DropIndex(
                name: "IX_Abonnementen_AccountID",
                table: "Abonnementen");

            migrationBuilder.DropColumn(
                name: "AccountID",
                table: "Abonnementen");

            migrationBuilder.RenameColumn(
                name: "Startdatum",
                table: "Abonnementen",
                newName: "AbonnementDate");
        }
    }
}
