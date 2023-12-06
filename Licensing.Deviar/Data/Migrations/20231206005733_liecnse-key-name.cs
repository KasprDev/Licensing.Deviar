using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    /// <inheritdoc />
    public partial class liecnsekeyname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Software",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LicenseKeys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "632c5d23-1b67-4c43-8c75-eee9d3cbb807", "AQAAAAIAAYagAAAAEFPprl4qZsfs0MOwjdCmCdWlDG/RogBCfScJ0IMMr7oJtn85IoE3W7FbNaoiF/noDQ==", "2cdd7204-4731-4190-8567-ab3f86f34c38" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LicenseKeys");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Software",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b67929d-9f4b-4488-b0fe-1987902ccb7e", "AQAAAAIAAYagAAAAEKckLk51yJTq55PM0CPk8CMxknUQdKFcCJfoZOiwiKoxdpYVTUWZg4GI9hisXvN4SA==", "da78594e-2d03-4125-bc53-d83c66aeb850" });
        }
    }
}
