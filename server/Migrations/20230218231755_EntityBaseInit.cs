using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulletin.Server.Migrations
{
    /// <inheritdoc />
    public partial class EntityBaseInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "ID",
                keyValue: "57d3f726-e91b-487d-8612-6898fbbcb158");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "ID",
                keyValue: "8921a0f6-005c-44e5-b29c-9b9fc32d9d43");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "UserRole",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Role",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Document",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Directory",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "DateCreated", "DateLastUpdated", "Name" },
                values: new object[,]
                {
                    { "036a7855-3fc1-4ea2-8862-2a957ca54759", new DateTime(2023, 2, 18, 23, 17, 54, 935, DateTimeKind.Utc).AddTicks(7196), new DateTime(2023, 2, 18, 23, 17, 54, 935, DateTimeKind.Utc).AddTicks(7196), "admin" },
                    { "50ba0e85-8889-4ea9-86cd-a1ed5f99b99c", new DateTime(2023, 2, 18, 23, 17, 54, 935, DateTimeKind.Utc).AddTicks(7219), new DateTime(2023, 2, 18, 23, 17, 54, 935, DateTimeKind.Utc).AddTicks(7219), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "ID",
                keyValue: "036a7855-3fc1-4ea2-8862-2a957ca54759");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "ID",
                keyValue: "50ba0e85-8889-4ea9-86cd-a1ed5f99b99c");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "User");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Directory");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "DateLastUpdated", "Name" },
                values: new object[,]
                {
                    { "57d3f726-e91b-487d-8612-6898fbbcb158", new DateTime(2023, 2, 18, 21, 18, 36, 479, DateTimeKind.Utc).AddTicks(9580), "admin" },
                    { "8921a0f6-005c-44e5-b29c-9b9fc32d9d43", new DateTime(2023, 2, 18, 21, 18, 36, 479, DateTimeKind.Utc).AddTicks(9603), "user" }
                });
        }
    }
}
