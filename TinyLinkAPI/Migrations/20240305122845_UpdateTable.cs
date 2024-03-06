using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyLink.API.Migrations
{
    public partial class UpdateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deativated",
                table: "TinyLinks",
                newName: "Deactivated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deactivated",
                table: "TinyLinks",
                newName: "Deativated");
        }
    }
}
