using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class licenseemail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "LicenseKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59996f98-24d0-49fc-b576-400154092352", "AQAAAAEAACcQAAAAEG6X9GTBAFcKQqyhV49Gayzh4N+w0X+TrC4APaPf+oCwOiis1Ek1kqEid6I+qBunqw==", "691acdfb-3056-42d4-9f15-2a6364b33497" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "LicenseKeys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05143337-cda2-4f21-bfc4-b34f92454071", "AQAAAAEAACcQAAAAEDSQFCicMxG4EVNAfw8oIzYjyprLV0rhTl9RWp0hrGbuHvh8ffrO+KyQ+sm3ivTvOg==", "0fc0e937-a7dd-43ab-9b58-102ae2dc0c5d" });
        }
    }
}
