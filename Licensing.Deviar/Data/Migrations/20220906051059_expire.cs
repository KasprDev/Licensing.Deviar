using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class expire : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiresOn",
                table: "LicenseKeys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "918f164f-fbca-4240-9017-40870c5fce1a", "AQAAAAEAACcQAAAAEOM+oqsaO+UP5y1Zv9cNVjKg7iILIjLzGeFB2gHcqQqdE5BeSJPeAO0RvqNIK5S7AA==", "3462bfe6-3340-44a0-950c-f3531586255e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiresOn",
                table: "LicenseKeys");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "371910a2-edd4-4063-8e89-7fecbde553f7", "AQAAAAEAACcQAAAAEGR7Tuyl5NmvWIi9yd812Ile3l4QHwTxrpVDfSKtrFAJE84ey5bVbGB5Hu+PjhGwBw==", "56c74120-711b-4e98-a9db-bce766d31c86" });
        }
    }
}
