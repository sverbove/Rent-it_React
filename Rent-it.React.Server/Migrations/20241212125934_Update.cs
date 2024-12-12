using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent_it.React.Server.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beschikbaar",
                table: "Voertuigen");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Beschikbaar",
                table: "Voertuigen",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
