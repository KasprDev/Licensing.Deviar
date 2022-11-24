using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class usagelogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsageLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsageLogs_LicenseKeys_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "LicenseKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5a727469-7705-46cc-a8cb-3e691b77a895", "AQAAAAEAACcQAAAAEOIiE+XYwX4kzn32kGVSctuU1ZnUDuQPQ7dby32wiNq0NmuVhx+uKYbfi95Dnxu67Q==", "185aaf4d-a53a-4cf0-bd75-8b5a0d5cf575" });

            migrationBuilder.CreateIndex(
                name: "IX_UsageLogs_LicenseId",
                table: "UsageLogs",
                column: "LicenseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsageLogs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "190e965d-dde1-4e57-8463-4be9c0244cd7", "AQAAAAEAACcQAAAAEH/gWiT7kuY1iMlTIM4hLVXEoTp1iGUjmNJU2suolFWfZHbQEQaRI++UTMFsyl5OOw==", "8d63db08-57f9-4ce0-9993-d6bac412bf1d" });
        }
    }
}
