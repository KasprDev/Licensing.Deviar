using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class nullhwid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HardwareId",
                table: "LicenseKeys",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05143337-cda2-4f21-bfc4-b34f92454071", "AQAAAAEAACcQAAAAEDSQFCicMxG4EVNAfw8oIzYjyprLV0rhTl9RWp0hrGbuHvh8ffrO+KyQ+sm3ivTvOg==", "0fc0e937-a7dd-43ab-9b58-102ae2dc0c5d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HardwareId",
                table: "LicenseKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "918f164f-fbca-4240-9017-40870c5fce1a", "AQAAAAEAACcQAAAAEOM+oqsaO+UP5y1Zv9cNVjKg7iILIjLzGeFB2gHcqQqdE5BeSJPeAO0RvqNIK5S7AA==", "3462bfe6-3340-44a0-950c-f3531586255e" });
        }
    }
}
