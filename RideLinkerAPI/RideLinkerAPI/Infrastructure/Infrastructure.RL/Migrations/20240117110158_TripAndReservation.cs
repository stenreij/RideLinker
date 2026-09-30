using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.RL.Migrations
{
    /// <inheritdoc />
    public partial class TripAndReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Locations_LocationId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 1, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 22, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 1, 23, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 23, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 1, 23, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 23, 15, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 1, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 27, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 1, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 27, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DriverId", "EndTime", "StartTime" },
                values: new object[] { 4, new DateTime(2024, 1, 27, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 27, 17, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "CarId", "DepartureId", "DestinationId", "DriverId", "EndTime", "StartTime" },
                values: new object[] { 7, 4, 1, 6, 4, new DateTime(2024, 2, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 1, 17, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "MobileNr", "Name", "UserRole" },
                values: new object[,]
                {
                    { 5, new DateTime(2001, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "quinn@mail.com", null, "Quinn", 0 },
                    { 6, new DateTime(2001, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "matthijs@mail.com", null, "Matthijs", 1 },
                    { 7, new DateTime(2001, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "baraa@mail.com", null, "Baraa", 1 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "TripId", "UserId" },
                values: new object[] { 7, 7, 4 });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "CarId", "DepartureId", "DestinationId", "DriverId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 8, 5, 1, 6, 5, new DateTime(2024, 1, 2, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 1, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 5, 6, 4, 5, new DateTime(2024, 2, 2, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 2, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 6, 4, 6, 6, new DateTime(2024, 2, 2, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 2, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 6, 6, 1, 6, new DateTime(2024, 2, 2, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 2, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 8, 2, 5, 7, new DateTime(2024, 2, 3, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 3, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 8, 5, 2, 7, new DateTime(2024, 2, 3, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 3, 17, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "TripId", "UserId" },
                values: new object[,]
                {
                    { 8, 8, 5 },
                    { 9, 9, 5 },
                    { 10, 10, 6 },
                    { 11, 11, 6 },
                    { 12, 12, 7 },
                    { 13, 13, 7 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Locations_LocationId",
                table: "Cars",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Locations_LocationId",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Reservations",
                keyColumn: "Id",
                keyValue: 6,
                column: "UserId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 12, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 22, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 12, 23, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 23, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 12, 23, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 23, 15, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EndTime", "StartTime" },
                values: new object[] { new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 12, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "DriverId", "EndTime", "StartTime" },
                values: new object[] { 2, new DateTime(2023, 12, 27, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 17, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Locations_LocationId",
                table: "Cars",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
