using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class sellyproductid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellyProductId",
                table: "Software",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7046c534-ac0e-4800-a6e9-ecfb5a2557da", "AQAAAAIAAYagAAAAEJJ3U3sCK4jyRKoIvFdFU5Nd5WT1lRH8tTUZ3mgC+2ji0E3GfkS4lchTTFkjKsqbqg==", "dbc5df49-5ebd-49af-8995-23a051b35c3f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellyProductId",
                table: "Software");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "52e43633-fef5-4711-aeea-45b756691ee5", "AQAAAAEAACcQAAAAEJJXNDtSZBjkqO+lltJ75Ebn0J9wM+wTEzS6FV7WQJKtlvCZMiC3r/NXNuBQGWgFwQ==", "88a8f549-a1b1-4672-91bc-913dfb57e81f" });
        }
    }
}
