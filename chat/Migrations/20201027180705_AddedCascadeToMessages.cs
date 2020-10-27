using Microsoft.EntityFrameworkCore.Migrations;

namespace chat.Migrations
{
    public partial class AddedCascadeToMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessagesDatabase_ChatsDatabase_ChatId",
                table: "MessagesDatabase");

            migrationBuilder.AddForeignKey(
                name: "FK_MessagesDatabase_ChatsDatabase_ChatId",
                table: "MessagesDatabase",
                column: "ChatId",
                principalTable: "ChatsDatabase",
                principalColumn: "ChatId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessagesDatabase_ChatsDatabase_ChatId",
                table: "MessagesDatabase");

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
