using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulletin.Server.Migrations
{
    /// <inheritdoc />
    public partial class PostgresMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateLastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Hash = table.Column<string>(type: "text", nullable: false),
                    SecurityToken = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateLastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Directory",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    ParentDirectoryID = table.Column<string>(type: "text", nullable: true),
                    UserID = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateLastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Directory_Directory_ParentDirectoryID",
                        column: x => x.ParentDirectoryID,
                        principalTable: "Directory",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Directory_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: false),
                    RoleID = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateLastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Filename = table.Column<string>(type: "text", nullable: false),
                    DirectoryID = table.Column<string>(type: "text", nullable: false),
                    UserID = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateLastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Document_Directory_DirectoryID",
                        column: x => x.DirectoryID,
                        principalTable: "Directory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "DateCreated", "DateLastUpdated", "Name" },
                values: new object[,]
                {
                    { "9f62f8a2-f7f0-4b06-b68e-c12ec5e56e5d", new DateTime(2023, 3, 2, 2, 23, 57, 270, DateTimeKind.Utc).AddTicks(1163), new DateTime(2023, 3, 2, 2, 23, 57, 270, DateTimeKind.Utc).AddTicks(1163), "user" },
                    { "f3921644-8b54-41a8-ada7-5856b0eaecfc", new DateTime(2023, 3, 2, 2, 23, 57, 270, DateTimeKind.Utc).AddTicks(1140), new DateTime(2023, 3, 2, 2, 23, 57, 270, DateTimeKind.Utc).AddTicks(1141), "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Directory_ParentDirectoryID",
                table: "Directory",
                column: "ParentDirectoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Directory_UserID",
                table: "Directory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DirectoryID",
                table: "Document",
                column: "DirectoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Document_UserID",
                table: "Document",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                table: "UserRole",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Directory");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
