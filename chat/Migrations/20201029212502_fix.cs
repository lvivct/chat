using Microsoft.EntityFrameworkCore.Migrations;

namespace chat.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "AddUsers",
                table: "ChatsUsersDatabase",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditChat",
                table: "ChatsUsersDatabase",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GiveRoles",
                table: "ChatsUsersDatabase",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "KickUsers",
                table: "ChatsUsersDatabase",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "ChatsUsersDatabase",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "ChatsUsersDatabase",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddUsers",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropColumn(
                name: "EditChat",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropColumn(
                name: "GiveRoles",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropColumn(
                name: "KickUsers",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "ChatsUsersDatabase");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "ChatsUsersDatabase");

            migrationBuilder.AddColumn<string>(
                name: "UserRoleRoleId",
                table: "ChatsUsersDatabase",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AddUsers = table.Column<bool>(type: "bit", nullable: false),
                    EditChat = table.Column<bool>(type: "bit", nullable: false),
                    GiveRoles = table.Column<bool>(type: "bit", nullable: false),
                    KickUsers = table.Column<bool>(type: "bit", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
