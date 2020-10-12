using Microsoft.EntityFrameworkCore.Migrations;

namespace chat.Migrations
{
    public partial class AppUserChatManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsDatabase_AspNetUsers_UserId",
                table: "ChatsDatabase");

            migrationBuilder.DropForeignKey(
                name: "FK_MessagesDatabase_ChatsDatabase_ChatId",
                table: "MessagesDatabase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatsDatabase",
                table: "ChatsDatabase");

            migrationBuilder.DropIndex(
                name: "IX_ChatsDatabase_UserId",
                table: "ChatsDatabase");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatsDatabase");

            migrationBuilder.RenameTable(
                name: "ChatsDatabase",
                newName: "Chat");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat",
                table: "Chat",
                column: "ChatId");

            migrationBuilder.CreateTable(
                name: "AppUserChat",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ChatId = table.Column<string>(nullable: false),
                    Id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserChat", x => new { x.ChatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_AppUserChat_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserChat_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserChat_UserId",
                table: "AppUserChat",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessagesDatabase_Chat_ChatId",
                table: "MessagesDatabase",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessagesDatabase_Chat_ChatId",
                table: "MessagesDatabase");

            migrationBuilder.DropTable(
                name: "AppUserChat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat",
                table: "Chat");

            migrationBuilder.RenameTable(
                name: "Chat",
                newName: "ChatsDatabase");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ChatsDatabase",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatsDatabase",
                table: "ChatsDatabase",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatsDatabase_UserId",
                table: "ChatsDatabase",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsDatabase_AspNetUsers_UserId",
                table: "ChatsDatabase",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MessagesDatabase_ChatsDatabase_ChatId",
                table: "MessagesDatabase",
                column: "ChatId",
                principalTable: "ChatsDatabase",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
