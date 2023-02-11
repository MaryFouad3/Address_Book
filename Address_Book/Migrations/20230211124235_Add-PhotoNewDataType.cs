using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Address_Book.Migrations
{
    public partial class AddPhotoNewDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "book",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "book");
        }
    }
}
