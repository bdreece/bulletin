using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bulletin.Server.Migrations
{
    /// <inheritdoc />
    public partial class UserTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directory_Users_UserID",
                table: "Directory");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Directory_DirectoryID",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users_UserID",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Document");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "User",
                newName: "SecurityToken");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_UserID",
                table: "Document",
                newName: "IX_Document_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_DirectoryID",
                table: "Document",
                newName: "IX_Document_DirectoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Directory_User_UserID",
                table: "Directory",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Directory_DirectoryID",
                table: "Document",
                column: "DirectoryID",
                principalTable: "Directory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_User_UserID",
                table: "Document",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directory_User_UserID",
                table: "Directory");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_Directory_DirectoryID",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_User_UserID",
                table: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_Email",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Documents");

            migrationBuilder.RenameColumn(
                name: "SecurityToken",
                table: "Users",
                newName: "Salt");

            migrationBuilder.RenameIndex(
                name: "IX_Document_UserID",
                table: "Documents",
                newName: "IX_Documents_UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Document_DirectoryID",
                table: "Documents",
                newName: "IX_Documents_DirectoryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Directory_Users_UserID",
                table: "Directory",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Directory_DirectoryID",
                table: "Documents",
                column: "DirectoryID",
                principalTable: "Directory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users_UserID",
                table: "Documents",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
