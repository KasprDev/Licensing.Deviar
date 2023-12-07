using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Deviar.Data.Migrations
{
    /// <inheritdoc />
    public partial class reselleramount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "ResellerLogs",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb903cf8-e595-4e45-901d-ee010fdeea8e", "AQAAAAIAAYagAAAAEDk0eYwQTrb46ZITKSbQAjLi+bSDrIDAWS/iLVVBnKzXhDdQMKdBn7FYGJUB92jT9g==", "ba973283-487a-4979-b1af-34ecd9e3e53d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ResellerLogs");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4eae93c9-dfb1-42fd-a72a-23e987cf06e0", "AQAAAAIAAYagAAAAEDW6Chuzcca2M46GH6ZFQwN94jFDgsLIF3JlPhLQup3bHMAeJn9tXit0NrevImSx0A==", "57c72ad9-eb4b-4e9a-b701-5b75aca14d58" });
        }
    }
}
