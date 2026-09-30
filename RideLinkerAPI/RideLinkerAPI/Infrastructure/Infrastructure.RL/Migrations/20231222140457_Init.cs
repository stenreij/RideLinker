using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.RL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MobileNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureId = table.Column<int>(type: "int", nullable: true),
                    DestinationId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: true),
                    DriverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trips_Locations_DepartureId",
                        column: x => x.DepartureId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trips_Locations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trips_Users_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TripId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Available", "Brand", "Image", "LocationId", "Model" },
                values: new object[,]
                {
                    { 1, true, "Volkswagen", "https://dam.broekhuis.online/online/broekhuis/modelpaginas/volkswagen/image-thumb__29831__original/hero-vw-id3-mob.webp", null, "ID.3" },
                    { 2, true, "Volkswagen", "https://dam.broekhuis.online/online/broekhuis/modelpaginas/volkswagen/image-thumb__29831__original/hero-vw-id3-mob.webp", null, "ID.3" },
                    { 3, true, "Volkswagen", "https://dam.broekhuis.online/online/broekhuis/modelpaginas/volkswagen/image-thumb__29831__original/hero-vw-id3-mob.webp", null, "ID.3" },
                    { 4, true, "Volkswagen", "https://dam.broekhuis.online/online/broekhuis/modelpaginas/volkswagen/image-thumb__29831__original/hero-vw-id3-mob.webp", null, "ID.3" },
                    { 5, true, "Audi", "https://ev-database.org/img/auto/Audi_e-tron/Audi_e-tron-01@2x.jpg", null, "E-tron" },
                    { 6, true, "Audi", "https://ev-database.org/img/auto/Audi_e-tron/Audi_e-tron-01@2x.jpg", null, "E-tron" },
                    { 7, true, "Audi", "https://ev-database.org/img/auto/Audi_e-tron/Audi_e-tron-01@2x.jpg", null, "E-tron" },
                    { 8, true, "Audi", "https://ev-database.org/img/auto/Audi_e-tron/Audi_e-tron-01@2x.jpg", null, "E-tron" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { 1, "Konijnenberg 24", "Breda" },
                    { 2, "Nachtwachtlaan 20", "Amsterdam" },
                    { 3, "Vliegend Hertlaan 39", "Utrecht" },
                    { 4, "Velperweg 27", "Arnhem" },
                    { 5, "Brouwerijstraat 1", "Enschede" },
                    { 6, "Vrijthof 23", "Maastricht" }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "CarId", "DepartureId", "DestinationId", "DriverId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, null, null, null, null, new DateTime(2023, 12, 22, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 22, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, null, null, null, new DateTime(2023, 12, 23, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 23, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, null, null, null, null, new DateTime(2023, 12, 23, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 23, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, null, null, null, null, new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, null, null, null, null, new DateTime(2023, 12, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, null, null, null, null, new DateTime(2023, 12, 27, 19, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 27, 17, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "BirthDate", "Email", "MobileNr", "Name", "UserRole" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@mail.com", null, "Admin", 0 },
                    { 2, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hg@mail.com", null, "Hans Gerard", 1 },
                    { 3, new DateTime(2000, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "sten@mail.com", null, "Sten", 1 },
                    { 4, new DateTime(2001, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kalle@mail.com", null, "Kalle", 0 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "TripId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 2 },
                    { 3, 3, 2 },
                    { 4, 4, 2 },
                    { 5, 5, 3 },
                    { 6, 6, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_LocationId",
                table: "Cars",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TripId",
                table: "Reservations",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId_TripId",
                table: "Reservations",
                columns: new[] { "UserId", "TripId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_CarId",
                table: "Trips",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DepartureId",
                table: "Trips",
                column: "DepartureId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationId",
                table: "Trips",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverId",
                table: "Trips",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
