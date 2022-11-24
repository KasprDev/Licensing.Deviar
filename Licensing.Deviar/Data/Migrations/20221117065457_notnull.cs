using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class notnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
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
                values: new object[] { "190e965d-dde1-4e57-8463-4be9c0244cd7", "AQAAAAEAACcQAAAAEH/gWiT7kuY1iMlTIM4hLVXEoTp1iGUjmNJU2suolFWfZHbQEQaRI++UTMFsyl5OOw==", "8d63db08-57f9-4ce0-9993-d6bac412bf1d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
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
                values: new object[] { "59996f98-24d0-49fc-b576-400154092352", "AQAAAAEAACcQAAAAEG6X9GTBAFcKQqyhV49Gayzh4N+w0X+TrC4APaPf+oCwOiis1Ek1kqEid6I+qBunqw==", "691acdfb-3056-42d4-9f15-2a6364b33497" });
        }
    }
}
