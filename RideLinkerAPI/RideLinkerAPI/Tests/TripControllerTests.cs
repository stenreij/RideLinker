using Core.Domain;
using Core.DomainService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RideLinkerAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class TripControllerTests
    {
        private readonly TripController _tripController;
        private readonly Mock<ILogger<TripController>> _loggerMock;
        private readonly Mock<ITripService> _tripServiceMock;
        private readonly Mock<IReservationService> _reservationServiceMock;
        private readonly ReservationController _reservationController;
        private readonly Mock<ILogger<ReservationController>> _reservationControllerMock;

        public TripControllerTests()
        {
            _loggerMock = new Mock<ILogger<TripController>>();
            _tripServiceMock = new Mock<ITripService>();
            _reservationServiceMock = new Mock<IReservationService>();
            _reservationControllerMock = new Mock<ILogger<ReservationController>>();

            _tripController = new TripController(
                _loggerMock.Object,
                _tripServiceMock.Object,
                _reservationServiceMock.Object
            );
        }

        private Trip CreateTestTrip(int id)
        {
            return new Trip
            {
                Id = id,
                Departure = new Location { Id = 1, Name = "LocationA" },
                Destination = new Location { Id = 2, Name = "LocationB" },
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Car = new Car { Id = 1, Brand = "TestCar" },
                Driver = new User { Id = 1, Name = "TestDriver" },
                Reservations = new List<Reservation>()
            };
        }

        [Fact]
        public async Task GetAllTrips_ReturnsOkResult()
        {
            // Arrange
            var trips = new List<Trip> { CreateTestTrip(1), CreateTestTrip(2) };
            _tripServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(trips);

            // Act
            var result = await _tripController.GetAllTrips();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<Trip>>(okResult.Value);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task GetTripById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var tripId = 1;
            var trip = CreateTestTrip(tripId);
            _tripServiceMock.Setup(service => service.GetByIdAsync(tripId)).ReturnsAsync(trip);

            // Act
            var result = await _tripController.GetTripById(tripId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Trip>(okResult.Value);
            Assert.Equal(tripId, model.Id);
            Assert.NotNull(model.Car);
            Assert.NotNull(model.Driver);
        }

        [Fact]
        public async Task GetTripById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var tripId = 1;
            _tripServiceMock.Setup(service => service.GetByIdAsync(tripId)).ReturnsAsync((Trip)null);

            // Act
            var result = await _tripController.GetTripById(tripId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddTrip_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var inTrip = new Trip { Id = 2, CarId = 2, DriverId = 3, DepartureId = 1, DestinationId = 2 };
            _tripServiceMock.Setup(service => service.AddAsync(inTrip)).Returns(Task.CompletedTask);

            // Act
            var result = await _tripController.AddTrip(inTrip);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Trip>(okResult.Value);
            Assert.Equal(inTrip.Id, model.Id);
            Assert.Equal(inTrip.CarId, model.CarId);
        }

        [Fact]
        public async Task UpdateTrip_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var tripId = 1;
            var updatedTrip = new Trip { Id = tripId };

            // Act
            var result = await _tripController.UpdateTrip(tripId, updatedTrip);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Trip>(okResult.Value);
            Assert.Equal(tripId, model.Id);
        }

        [Fact]
        public async Task UpdateTrip_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            var tripId = 1;
            Trip updatedTrip = null;

            // Act
            var result = await _tripController.UpdateTrip(tripId, updatedTrip);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteTrip_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var tripId = 1;
            var trips = new List<Trip> { new Trip { Id = 1 }, new Trip { Id = 2 } };
            _tripServiceMock.Setup(service => service.GetByIdAsync(tripId)).ReturnsAsync(trips.FirstOrDefault(t => t.Id == tripId));
            _tripServiceMock.Setup(service => service.DeleteAsync(tripId)).Returns(Task.CompletedTask);

            // Act
            var result = await _tripController.DeleteTrip(tripId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Trip ID gedelete: {tripId}", okResult.Value);
        }

        [Fact]
        public async Task DeleteTrip_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingTripId = 0;
            _tripServiceMock.Setup(service => service.GetByIdAsync(nonExistingTripId)).ReturnsAsync((Trip)null);

            // Act
            var result = await _tripController.DeleteTrip(nonExistingTripId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
        }
    }
}
