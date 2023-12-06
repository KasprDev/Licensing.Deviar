using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    /// <inheritdoc />
    public partial class resellers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResellerLogId",
                table: "LicenseKeys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Resellers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    Added = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resellers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resellers_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResellerLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResellerId = table.Column<int>(type: "int", nullable: false),
                    LicenseKeyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResellerLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResellerLogs_LicenseKeys_LicenseKeyId",
                        column: x => x.LicenseKeyId,
                        principalTable: "LicenseKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResellerLogs_Resellers_ResellerId",
                        column: x => x.ResellerId,
                        principalTable: "Resellers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "873d7a43-659e-4b78-af23-564bc4ef7f9a", "AQAAAAIAAYagAAAAEPjsfapq4dlZnLYjtRkmphdTtfWqC6LsMVS7hYA19D3Pk09aVPEUEirhCJRmuOpTiw==", "d8c09e83-2463-4178-8194-0686d6b02b62" });

            migrationBuilder.CreateIndex(
                name: "IX_ResellerLogs_LicenseKeyId",
                table: "ResellerLogs",
                column: "LicenseKeyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResellerLogs_ResellerId",
                table: "ResellerLogs",
                column: "ResellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Resellers_SoftwareId",
                table: "Resellers",
                column: "SoftwareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResellerLogs");

            migrationBuilder.DropTable(
                name: "Resellers");

            migrationBuilder.DropColumn(
                name: "ResellerLogId",
                table: "LicenseKeys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "632c5d23-1b67-4c43-8c75-eee9d3cbb807", "AQAAAAIAAYagAAAAEFPprl4qZsfs0MOwjdCmCdWlDG/RogBCfScJ0IMMr7oJtn85IoE3W7FbNaoiF/noDQ==", "2cdd7204-4731-4190-8567-ab3f86f34c38" });
        }
    }
}
