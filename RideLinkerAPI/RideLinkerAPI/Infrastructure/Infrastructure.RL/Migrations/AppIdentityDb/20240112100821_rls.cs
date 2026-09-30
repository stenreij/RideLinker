using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.RL.Migrations.AppIdentityDb
{
    /// <inheritdoc />
    public partial class rls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "ADMIN", "ADMIN" },
                    { "2", null, "USER", "USER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d14eb52-0557-47a5-9a87-fed4ba8b83be", "AQAAAAIAAYagAAAAEGO8hN09MUFi8DDyjETf5YwqZcIGJ8VzEn2xM7h8Lo3P3BwuASaEZjRLAcs4Jx0/oA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fab4fac1-c546-41de-aebc-a14da6895711", null, "ADMIN", "ADMIN" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e64384bf-c5a0-405b-a39d-89ae37dda323", "AQAAAAIAAYagAAAAENLIOVYmNrVZbuHDfM7iOZPbTiAM66SoC9fmABmmBsmIHRTnC1fPMwbRfjHiilqKLg==" });
        }
    }
}
