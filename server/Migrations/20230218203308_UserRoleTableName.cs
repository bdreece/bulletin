using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulletin.Server.Migrations
{
    /// <inheritdoc />
    public partial class UserRoleTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Role_RoleID",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_User_UserID",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRole");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_UserID",
                table: "UserRole",
                newName: "IX_UserRole_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleID",
                table: "UserRole",
                newName: "IX_UserRole_RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleID",
                table: "UserRole",
                column: "RoleID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserID",
                table: "UserRole",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleID",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserID",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_UserID",
                table: "UserRoles",
                newName: "IX_UserRoles_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleID",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Role_RoleID",
                table: "UserRoles",
                column: "RoleID",
                principalTable: "Role",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_User_UserID",
                table: "UserRoles",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
