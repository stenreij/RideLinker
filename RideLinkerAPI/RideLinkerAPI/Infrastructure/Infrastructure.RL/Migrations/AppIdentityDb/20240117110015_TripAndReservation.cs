using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.RL.Migrations.AppIdentityDb
{
    /// <inheritdoc />
    public partial class TripAndReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "50e0c7ca-ae32-4fc4-aca6-9c2063b33bc1", "AQAAAAIAAYagAAAAEPf0Pk4cSxyh+89ZeB4euZtcBQmt6M7e1JK0P2Hu27NylTRIlUxx/Ef5gmZeEHlLUA==" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2", 0, "5170ad75-3950-492c-92f5-3538e9243a1f", "hg@mail.com", true, false, null, "HG@MAIL.COM", "HG@MAIL.COM", "AQAAAAIAAYagAAAAEEJykIy1efTA2cTzV71FqX2TcJAprPZUVFhMwzWu3adF4lApAlHxUtN7otDJzXeUnQ==", null, false, "", false, "hg@mail.com" },
                    { "3", 0, "efda7153-197b-48bc-b377-e227fe33c988", "sten@mail.com", true, false, null, "STEN@MAIL.COM", "STEN@MAIL.COM", "AQAAAAIAAYagAAAAEGS9Xn13biQrHkwW6qdLGqsXx8qnhlvIhF3yNrPc+BhJsEDNjTQoSRqMuUk5CSJxRA==", null, false, "", false, "sten@mail.com" },
                    { "4", 0, "54c6a9b7-e69f-44e4-9dc8-b421851d190c", "kalle@mail.com", true, false, null, "KALLE@MAIL.COM", "KALLE@MAIL.COM", "AQAAAAIAAYagAAAAEAH+q8Aiqmj+V9jmGPLrXDji+WM7ZSAgikOkvTxKbEgAWFlaz8dKzNAWK44j9KIYlw==", null, false, "", false, "kalle@mail.com" },
                    { "5", 0, "1e3ec81d-4c55-48ae-83c1-b7890931fff0", "quinn@mail.com", true, false, null, "QUINN@MAIL.COM", "QUINN@MAIL.COM", "AQAAAAIAAYagAAAAEE26WnHbrT/RFEyYpJv4bkr7kd6bLxqMTWNpAw+Ff1CSLVkI7oDRmw80ug429Yxu8w==", null, false, "", false, "quinn@mail.com" },
                    { "6", 0, "174e8948-9545-4066-8f35-dfe93b501440", "matthijs@mail.com", true, false, null, "MATTHIJS@MAIL.COM", "MATTHIJS@MAIL.COM", "AQAAAAIAAYagAAAAEDighDA4gkLNH6DIN67eEZCsXh1xoEDymiQX55kkT1LokayMvvrMiKMIShHsv2gbpw==", null, false, "", false, "matthijs@mail.com" },
                    { "7", 0, "a2108d0f-a92f-42c9-b908-8bc883d87b46", "baraa@mail.com", true, false, null, "BARAA@MAIL.COM", "BARAA@MAIL.COM", "AQAAAAIAAYagAAAAEG1dJVqr6eAAdXGGcMMj2AjQacFKQneb/zRO0WjJSYMSdDC4WG1DJhuo86+xE1+vtQ==", null, false, "", false, "baraa@mail.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d14eb52-0557-47a5-9a87-fed4ba8b83be", "AQAAAAIAAYagAAAAEGO8hN09MUFi8DDyjETf5YwqZcIGJ8VzEn2xM7h8Lo3P3BwuASaEZjRLAcs4Jx0/oA==" });
        }
    }
}
