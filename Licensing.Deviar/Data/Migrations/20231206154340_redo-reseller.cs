using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    /// <inheritdoc />
    public partial class redoreseller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reseller",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "ResellerId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ResellerId", "SecurityStamp" },
                values: new object[] { "fecd2e51-195d-4183-b2b1-7deac1783b6d", "AQAAAAIAAYagAAAAEDBGPhFp1tCcwyoWc2kVejQD8T4+g8NSlTMcwESrKh29UPzdGiX5hlYNuzR/qAs3HA==", null, "6f89f5ca-f912-4731-b7c0-00a72d50a211" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ResellerId",
                table: "AspNetUsers",
                column: "ResellerId",
                unique: true,
                filter: "[ResellerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Resellers_ResellerId",
                table: "AspNetUsers",
                column: "ResellerId",
                principalTable: "Resellers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Resellers_ResellerId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ResellerId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResellerId",
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
    }
}
