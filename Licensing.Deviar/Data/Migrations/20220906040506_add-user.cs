using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    public partial class adduser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b74ddd14-6340-4840-95c2-db12554843e5", 0, "e1430239-8368-41a9-94f9-d00bf3fe2589", "contact@deviar.net", false, false, null, null, null, "AQAAAAEAACcQAAAAEMyGKNYLHWt7uKYdeBrTOnJCfMVXqo9239OE238HgxyY1DN7nSEJ+hoE4h4jzCHZFQ==", null, false, "ffa9ab6a-af2c-4940-922a-1ce1cd174719", false, "contact@deviar.net" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4cf76891-0251-4626-9437-fefd5759df1e", 0, "7aced249-28ee-4038-b3a6-56cae3c39d58", "contact@deviar.net", false, false, null, null, null, null, null, false, "601bf548-dad1-4c55-89e2-2d6f05e44106", false, "contact@deviar.net" });
        }
    }
}
