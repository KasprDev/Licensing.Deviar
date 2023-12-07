using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    /// <inheritdoc />
    public partial class redoreseller33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resellers_Software_SoftwareId",
                table: "Resellers");

            migrationBuilder.DropIndex(
                name: "IX_Resellers_SoftwareId",
                table: "Resellers");

            migrationBuilder.CreateTable(
                name: "ResellerSoftware",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftwareId = table.Column<int>(type: "int", nullable: false),
                    ResellerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResellerSoftware", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResellerSoftware_Resellers_ResellerId",
                        column: x => x.ResellerId,
                        principalTable: "Resellers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ResellerSoftware_Software_SoftwareId",
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
                values: new object[] { "4eae93c9-dfb1-42fd-a72a-23e987cf06e0", "AQAAAAIAAYagAAAAEDW6Chuzcca2M46GH6ZFQwN94jFDgsLIF3JlPhLQup3bHMAeJn9tXit0NrevImSx0A==", "57c72ad9-eb4b-4e9a-b701-5b75aca14d58" });

            migrationBuilder.CreateIndex(
                name: "IX_ResellerSoftware_ResellerId",
                table: "ResellerSoftware",
                column: "ResellerId");

            migrationBuilder.CreateIndex(
                name: "IX_ResellerSoftware_SoftwareId",
                table: "ResellerSoftware",
                column: "SoftwareId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResellerSoftware");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fecd2e51-195d-4183-b2b1-7deac1783b6d", "AQAAAAIAAYagAAAAEDBGPhFp1tCcwyoWc2kVejQD8T4+g8NSlTMcwESrKh29UPzdGiX5hlYNuzR/qAs3HA==", "6f89f5ca-f912-4731-b7c0-00a72d50a211" });

            migrationBuilder.CreateIndex(
                name: "IX_Resellers_SoftwareId",
                table: "Resellers",
                column: "SoftwareId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resellers_Software_SoftwareId",
                table: "Resellers",
                column: "SoftwareId",
                principalTable: "Software",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
