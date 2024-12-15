using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent_it.React.Server.Migrations
{
    /// <inheritdoc />
    public partial class AbonnementDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    AbonnementDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnementen", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonnementen");
        }
    }
}
