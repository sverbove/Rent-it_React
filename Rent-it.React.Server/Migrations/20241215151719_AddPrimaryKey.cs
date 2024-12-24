using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent_it.React.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accounts",
                newName: "AccountID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AccountID",
                table: "Accounts",
                newName: "Id");
        }
    }
}
