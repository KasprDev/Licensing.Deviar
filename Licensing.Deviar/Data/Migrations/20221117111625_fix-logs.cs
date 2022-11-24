using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class fixlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsageLogs_LicenseKeys_LicenseId",
                table: "UsageLogs");

            migrationBuilder.DropIndex(
                name: "IX_UsageLogs_LicenseId",
                table: "UsageLogs");

            migrationBuilder.DropColumn(
                name: "LicenseId",
                table: "UsageLogs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b94b4768-9e3a-4410-8e15-01c3c619dd6e", "AQAAAAEAACcQAAAAEA4XmKqAHqN3bCFQG4j5RaiPoZ0e+ZxDvZnUoK4mIKgHL6cR09CHHAOSV4Zj9RUE6w==", "364434ea-07e6-42f2-8488-2265a3ce3990" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LicenseId",
                table: "UsageLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_UsageLogs_LicenseKeys_LicenseId",
                table: "UsageLogs",
                column: "LicenseId",
                principalTable: "LicenseKeys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
