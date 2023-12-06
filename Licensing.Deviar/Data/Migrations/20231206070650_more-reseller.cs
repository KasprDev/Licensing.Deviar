using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    /// <inheritdoc />
    public partial class morereseller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResellerOf",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee3814c8-e214-418e-bebe-c7c876dcb127", "AQAAAAIAAYagAAAAELwCcfiWDdlTKu5ZBYv0+1MpkEmg5m7mUidCKceyt3iSKAYpqLzBbFeIjKCoNrEh4A==", "80710124-ec52-439c-bc3f-43cead704724" });
        }
    }
}
