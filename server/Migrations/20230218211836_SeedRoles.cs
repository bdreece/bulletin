using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bulletin.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "ID", "DateLastUpdated", "Name" },
                values: new object[,]
                {
                    { "57d3f726-e91b-487d-8612-6898fbbcb158", new DateTime(2023, 2, 18, 21, 18, 36, 479, DateTimeKind.Utc).AddTicks(9580), "admin" },
                    { "8921a0f6-005c-44e5-b29c-9b9fc32d9d43", new DateTime(2023, 2, 18, 21, 18, 36, 479, DateTimeKind.Utc).AddTicks(9603), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "ID",
                keyValue: "57d3f726-e91b-487d-8612-6898fbbcb158");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "ID",
                keyValue: "8921a0f6-005c-44e5-b29c-9b9fc32d9d43");
        }
    }
}
