using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.RL.Migrations
{
    /// <inheritdoc />
    public partial class car : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cars_CarId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Locations_DepartureId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Locations_DestinationId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_DriverId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartureId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7,
                column: "LocationId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                column: "LocationId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { 1, 1, 2, 2 });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { 1, 2, 1, 2 });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { 2, 1, 5, 3 });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { 2, 5, 1, 3 });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { 3, 1, 2, 1 });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { 4, 1, 6, 2 });

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cars_CarId",
                table: "Trips",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Locations_DepartureId",
                table: "Trips",
                column: "DepartureId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Locations_DestinationId",
                table: "Trips",
                column: "DestinationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_DriverId",
                table: "Trips",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Cars_CarId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Locations_DepartureId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Locations_DestinationId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Users_DriverId",
                table: "Trips");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DepartureId",
                table: "Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "Trips",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1,
                column: "LocationId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2,
                column: "LocationId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3,
                column: "LocationId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4,
                column: "LocationId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5,
                column: "LocationId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6,
                column: "LocationId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7,
                column: "LocationId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8,
                column: "LocationId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Trips",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CarId", "DepartureId", "DestinationId", "DriverId" },
                values: new object[] { null, null, null, null });

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Cars_CarId",
                table: "Trips",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Locations_DepartureId",
                table: "Trips",
                column: "DepartureId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Locations_DestinationId",
                table: "Trips",
                column: "DestinationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Users_DriverId",
                table: "Trips",
                column: "DriverId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
