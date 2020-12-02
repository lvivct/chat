using Microsoft.EntityFrameworkCore.Migrations;

namespace chat.Migrations
{
    public partial class PhotoPathInMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "MessagesDatabase",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "MessagesDatabase");
        }
    }
}
