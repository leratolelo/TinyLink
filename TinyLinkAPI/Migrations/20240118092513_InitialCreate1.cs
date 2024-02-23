using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TinyLink.API.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TinyLinks",
                table: "TinyLinks");

            migrationBuilder.DropColumn(
                name: "TinyLinkId",
                table: "TinyLinks");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TinyLinks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ShortLink",
                table: "TinyLinks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TinyLinks",
                table: "TinyLinks",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TinyLinks",
                table: "TinyLinks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TinyLinks");

            migrationBuilder.DropColumn(
                name: "ShortLink",
                table: "TinyLinks");

            migrationBuilder.AddColumn<int>(
                name: "TinyLinkId",
                table: "TinyLinks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TinyLinks",
                table: "TinyLinks",
                column: "TinyLinkId");
        }
    }
}
