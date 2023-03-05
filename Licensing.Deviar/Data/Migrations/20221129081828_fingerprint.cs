using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class fingerprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceName",
                table: "LicenseKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "LicenseKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MacAddress",
                table: "LicenseKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7c98dcc-d89d-46cc-845a-d0ca674d6130", "AQAAAAEAACcQAAAAEIWikw8dLG2vjndeY3REXuhehSZLW9eESLKckNqKKz6QMMFO0KuHE9ig/IU5e4j2lg==", "96cec196-af2a-47dd-8dee-daad414c3396" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceName",
                table: "LicenseKeys");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "LicenseKeys");

            migrationBuilder.DropColumn(
                name: "MacAddress",
                table: "LicenseKeys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b94b4768-9e3a-4410-8e15-01c3c619dd6e", "AQAAAAEAACcQAAAAEA4XmKqAHqN3bCFQG4j5RaiPoZ0e+ZxDvZnUoK4mIKgHL6cR09CHHAOSV4Zj9RUE6w==", "364434ea-07e6-42f2-8488-2265a3ce3990" });
        }
    }
}
