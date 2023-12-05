using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class notes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "LicenseKeys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b1502b4-96df-49f9-b7ed-129986fe479b", "AQAAAAIAAYagAAAAEKh8LX1y1yAeCYM49lSJTodM4MtXQwqojO+hflnMxCw2D4GbcXF+DHNSQWRhlfQFOQ==", "eae018c3-787d-43e3-867b-852ddc3baf6b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "LicenseKeys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "57c41bd9-43c2-4fda-8f84-c31534f997ba", "AQAAAAIAAYagAAAAEMFsB+4CsIzD1/vBSc4mBNkeFIieWJyw+VCk5xGWnwSG3h3tKcrn0MSbLGV9j/9a2w==", "f3c034bf-3e15-4829-8695-7defa07db000" });
        }
    }
}
