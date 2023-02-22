#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Pijze.Infrastructure.Data.EntityFramework.Migrations
{
    public partial class BeerCreationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Beers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Beers");
        }
    }
}
