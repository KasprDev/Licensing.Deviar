using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class stripeid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SellyProductId",
                table: "Software",
                newName: "StripeProductId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e8289bb-d50e-4eb6-a79c-3c04e39840c2", "AQAAAAIAAYagAAAAEBjnWVd3hSq2b2kfMXrepv+sG1v9uQcY+vQ1glevn5ouE56oHFukS4XOs21fTS7NSg==", "041b7a7e-517e-4cc8-b76c-557528fbe025" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StripeProductId",
                table: "Software",
                newName: "SellyProductId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b1502b4-96df-49f9-b7ed-129986fe479b", "AQAAAAIAAYagAAAAEKh8LX1y1yAeCYM49lSJTodM4MtXQwqojO+hflnMxCw2D4GbcXF+DHNSQWRhlfQFOQ==", "eae018c3-787d-43e3-867b-852ddc3baf6b" });
        }
    }
}
