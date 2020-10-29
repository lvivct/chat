using Microsoft.EntityFrameworkCore.Migrations;

namespace chat.Migrations
{
    public partial class AddedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ChatsUsersDatabase");

            migrationBuilder.AddColumn<string>(
                name: "UserRoleRoleId",
                table: "ChatsUsersDatabase",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<string>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    KickUsers = table.Column<bool>(nullable: false),
                    GiveRoles = table.Column<bool>(nullable: false),
                    EditChat = table.Column<bool>(nullable: false),
                    AddUsers = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatsUsersDatabase_UserRoleRoleId",
                table: "ChatsUsersDatabase",
                column: "UserRoleRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatsUsersDatabase_Role_UserRoleRoleId",
                table: "ChatsUsersDatabase",
                column: "UserRoleRoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatsUsersDatabase_Role_UserRoleRoleId",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropIndex(
                name: "IX_ChatsUsersDatabase_UserRoleRoleId",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleId",
                table: "ChatsUsersDatabase");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ChatsUsersDatabase",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
