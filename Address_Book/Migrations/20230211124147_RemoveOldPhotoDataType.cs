using Microsoft.EntityFrameworkCore.Migrations;

namespace Address_Book.Migrations
{
    public partial class RemoveOldPhotoDataType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "book");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "book",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
