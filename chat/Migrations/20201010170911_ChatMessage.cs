using Microsoft.EntityFrameworkCore.Migrations;

namespace chat.Migrations
{
    public partial class ChatMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessagesDatabase",
                columns: table => new
                {
                    MessageId = table.Column<string>(nullable: false),
                    MessageText = table.Column<string>(nullable: false),
                    SenderName = table.Column<string>(nullable: true),
                    ChatId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesDatabase", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_MessagesDatabase_ChatsDatabase_ChatId",
                        column: x => x.ChatId,
                        principalTable: "ChatsDatabase",
                        principalColumn: "ChatId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessagesDatabase_ChatId",
                table: "MessagesDatabase",
                column: "ChatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessagesDatabase");
        }
    }
}
