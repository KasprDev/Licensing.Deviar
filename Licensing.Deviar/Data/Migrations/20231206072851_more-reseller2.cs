using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    /// <inheritdoc />
    public partial class morereseller2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResellerOf",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "Reseller",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Reseller", "SecurityStamp" },
                values: new object[] { "bd7ba3de-b82c-4b1a-b588-2df8799b8084", "AQAAAAIAAYagAAAAEIb8l+bHnwbyLLhSuuJvx5t0y9ZKa4tA8F8bX802bYEtsh8CCSsrt5fwpezaInqT6g==", false, "c365d017-cc81-4c5c-b02a-237df7f6e1e9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reseller",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ResellerOf",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ResellerOf", "SecurityStamp" },
                values: new object[] { "8d9c94f6-7e8d-496a-980b-9f8aed769213", "AQAAAAIAAYagAAAAELla/bxUgAsH+IDyEESXNUnZ+I+gISasR1bZ+is4bmahjVfj/UtB8hgS7yDOSd4BGw==", null, "b3903b18-1fa6-4336-84aa-4f94c4d14d1d" });
        }
    }
}
