using Microsoft.EntityFrameworkCore.Migrations;

namespace chat.Migrations
{
    public partial class PhotoPathInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Photopath",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photopath",
                table: "AspNetUsers");
        }
    }
}
