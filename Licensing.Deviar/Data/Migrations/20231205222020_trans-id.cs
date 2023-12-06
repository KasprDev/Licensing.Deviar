using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class transid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "LicenseKeys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b67929d-9f4b-4488-b0fe-1987902ccb7e", "AQAAAAIAAYagAAAAEKckLk51yJTq55PM0CPk8CMxknUQdKFcCJfoZOiwiKoxdpYVTUWZg4GI9hisXvN4SA==", "da78594e-2d03-4125-bc53-d83c66aeb850" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "LicenseKeys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e8289bb-d50e-4eb6-a79c-3c04e39840c2", "AQAAAAIAAYagAAAAEBjnWVd3hSq2b2kfMXrepv+sG1v9uQcY+vQ1glevn5ouE56oHFukS4XOs21fTS7NSg==", "041b7a7e-517e-4cc8-b76c-557528fbe025" });
        }
    }
}
