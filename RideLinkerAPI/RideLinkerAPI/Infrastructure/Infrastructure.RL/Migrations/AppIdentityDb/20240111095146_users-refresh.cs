using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.RL.Migrations.AppIdentityDb
{
    /// <inheritdoc />
    public partial class usersrefresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c7b013f0-5201-4317-abd8-c211f91b7330");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "Name",
                value: "ADMIN");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "e64384bf-c5a0-405b-a39d-89ae37dda323", "admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAIAAYagAAAAENLIOVYmNrVZbuHDfM7iOZPbTiAM66SoC9fmABmmBsmIHRTnC1fPMwbRfjHiilqKLg==", null, false, "", false, "admin@mail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fab4fac1-c546-41de-aebc-a14da6895711",
                column: "Name",
                value: "Admin");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c7b013f0-5201-4317-abd8-c211f91b7330", 0, "e951e49b-95b2-4cf6-9052-7de430cfc8c0", "Admin@mail.com", true, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAIAAYagAAAAEJS7TgPH25B8/sl8QRHJYySNM0zNMKa/VyoQtqaHYH8SSz4U1J56lZH+JsVnDYRK/A==", null, false, "", false, "Admin@mail.com" });
        }
    }
}
