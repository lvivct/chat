using Microsoft.EntityFrameworkCore.Migrations;

namespace chat.Migrations
{
    public partial class AppUserChat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatsDatabase",
                columns: table => new
                {
                    ChatId = table.Column<string>(nullable: false),
                    ChatName = table.Column<string>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatsDatabase", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_ChatsDatabase_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatsDatabase_UserId",
                table: "ChatsDatabase",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatsDatabase");
        }
    }
}
