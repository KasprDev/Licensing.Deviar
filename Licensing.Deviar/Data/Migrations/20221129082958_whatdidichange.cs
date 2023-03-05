using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class whatdidichange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "054bfa61-64ab-4370-b380-41cd64d51709", "AQAAAAEAACcQAAAAEMWbaacvn+exZeXCgG3UlxsrTuXf41rUa39vyuQI0MprlWmBEXB3XSKoHf0A5h5Xdw==", "a459b436-642c-4f4c-a052-a982e05c2d78" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7c98dcc-d89d-46cc-845a-d0ca674d6130", "AQAAAAEAACcQAAAAEIWikw8dLG2vjndeY3REXuhehSZLW9eESLKckNqKKz6QMMFO0KuHE9ig/IU5e4j2lg==", "96cec196-af2a-47dd-8dee-daad414c3396" });
        }
    }
}
