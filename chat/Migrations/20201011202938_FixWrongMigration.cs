using Microsoft.EntityFrameworkCore.Migrations;

namespace chat.Migrations
{
    public partial class FixWrongMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserChat_Chat_ChatId",
                table: "AppUserChat");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserChat_AspNetUsers_UserId",
                table: "AppUserChat");

            migrationBuilder.DropForeignKey(
                name: "FK_MessagesDatabase_Chat_ChatId",
                table: "MessagesDatabase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Chat",
                table: "Chat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserChat",
                table: "AppUserChat");

            migrationBuilder.RenameTable(
                name: "Chat",
                newName: "ChatsDatabase");

            migrationBuilder.RenameTable(
                name: "AppUserChat",
                newName: "ChatsUsersDatabase");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserChat_UserId",
                table: "ChatsUsersDatabase",
                newName: "IX_ChatsUsersDatabase_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatsDatabase",
                table: "ChatsDatabase",
                column: "ChatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatsUsersDatabase",
                table: "ChatsUsersDatabase",
                columns: new[] { "ChatId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsUsersDatabase_ChatsDatabase_ChatId",
                table: "ChatsUsersDatabase",
                column: "ChatId",
                principalTable: "ChatsDatabase",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsUsersDatabase_AspNetUsers_UserId",
                table: "ChatsUsersDatabase",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessagesDatabase_ChatsDatabase_ChatId",
                table: "MessagesDatabase",
                column: "ChatId",
                principalTable: "ChatsDatabase",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsUsersDatabase_ChatsDatabase_ChatId",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatsUsersDatabase_AspNetUsers_UserId",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropForeignKey(
                name: "FK_MessagesDatabase_ChatsDatabase_ChatId",
                table: "MessagesDatabase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatsUsersDatabase",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatsDatabase",
                table: "ChatsDatabase");

            migrationBuilder.RenameTable(
                name: "ChatsUsersDatabase",
                newName: "AppUserChat");

            migrationBuilder.RenameTable(
                name: "ChatsDatabase",
                newName: "Chat");

            migrationBuilder.RenameIndex(
                name: "IX_ChatsUsersDatabase_UserId",
                table: "AppUserChat",
                newName: "IX_AppUserChat_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserChat",
                table: "AppUserChat",
                columns: new[] { "ChatId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chat",
                table: "Chat",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserChat_Chat_ChatId",
                table: "AppUserChat",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserChat_AspNetUsers_UserId",
                table: "AppUserChat",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessagesDatabase_Chat_ChatId",
                table: "MessagesDatabase",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
