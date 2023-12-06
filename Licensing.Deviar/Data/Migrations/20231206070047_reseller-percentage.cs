using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    /// <inheritdoc />
    public partial class resellerpercentage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Resellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "Resellers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee3814c8-e214-418e-bebe-c7c876dcb127", "AQAAAAIAAYagAAAAELwCcfiWDdlTKu5ZBYv0+1MpkEmg5m7mUidCKceyt3iSKAYpqLzBbFeIjKCoNrEh4A==", "80710124-ec52-439c-bc3f-43cead704724" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Resellers");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Resellers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "873d7a43-659e-4b78-af23-564bc4ef7f9a", "AQAAAAIAAYagAAAAEPjsfapq4dlZnLYjtRkmphdTtfWqC6LsMVS7hYA19D3Pk09aVPEUEirhCJRmuOpTiw==", "d8c09e83-2463-4178-8194-0686d6b02b62" });
        }
    }
}
