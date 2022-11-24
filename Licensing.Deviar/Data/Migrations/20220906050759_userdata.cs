using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class userdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Software",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Software", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Software_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LicenseKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HardwareId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUsed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false),
                    SoftwareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LicenseKeys_Software_SoftwareId",
                        column: x => x.SoftwareId,
                        principalTable: "Software",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "371910a2-edd4-4063-8e89-7fecbde553f7", "AQAAAAEAACcQAAAAEGR7Tuyl5NmvWIi9yd812Ile3l4QHwTxrpVDfSKtrFAJE84ey5bVbGB5Hu+PjhGwBw==", "56c74120-711b-4e98-a9db-bce766d31c86" });

            migrationBuilder.CreateIndex(
                name: "IX_LicenseKeys_SoftwareId",
                table: "LicenseKeys",
                column: "SoftwareId");

            migrationBuilder.CreateIndex(
                name: "IX_Software_UserId",
                table: "Software",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LicenseKeys");

            migrationBuilder.DropTable(
                name: "Software");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e1430239-8368-41a9-94f9-d00bf3fe2589", "AQAAAAEAACcQAAAAEMyGKNYLHWt7uKYdeBrTOnJCfMVXqo9239OE238HgxyY1DN7nSEJ+hoE4h4jzCHZFQ==", "ffa9ab6a-af2c-4940-922a-1ce1cd174719" });
        }
    }
}
