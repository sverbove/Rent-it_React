using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent_it.React.Server.Migrations
{
    /// <inheritdoc />
    public partial class MedewerkerAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "Accounts",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "ParentAccountId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentAccountId",
                table: "Accounts",
                column: "ParentAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_ParentAccountId",
                table: "Accounts",
                column: "ParentAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ParentAccountId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ParentAccountId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ParentAccountId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accounts",
                newName: "AccountID");
        }
    }
}
