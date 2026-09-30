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
    public class ReservationControllerTests
    {
        private readonly ReservationController _reservationController;
        private readonly UserController _userController;
        private readonly Mock<ILogger<UserController>> _userControllerMock;
        private readonly TripController _tripController;
        private readonly Mock<ILogger<TripController>> _tripControllerMock;
        private readonly Mock<ILogger<ReservationController>> _loggerMock;
        private readonly Mock<IReservationService> _reservationServiceMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ITripService> _tripServiceMock;

        public ReservationControllerTests()
        {
            _loggerMock = new Mock<ILogger<ReservationController>>();
            _reservationServiceMock = new Mock<IReservationService>();
            _userServiceMock = new Mock<IUserService>();
            _tripServiceMock = new Mock<ITripService>();

            _reservationController = new ReservationController(
                _loggerMock.Object, 
                _reservationServiceMock.Object,
                _userServiceMock.Object,
                _tripServiceMock.Object
            );
        }

        private Reservation CreateTestReservation(int id)
        {
            return new Reservation
            {
                Id = id,
                UserId = 1,
                TripId = 1,
                User = new User { Id = 1, Name = "TestUser" },
                Trip = new Trip { Id = 1, Departure = new Location(), Destination = new Location() }
            };
        }

        [Fact]
        public async Task GetAllReservations_ReturnsOkResult()
        {
            // Arrange
            var reservations = new List<Reservation> { CreateTestReservation(1), CreateTestReservation(2) };
            _reservationServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(reservations);

            // Act
            var result = await _reservationController.GetAllReservations();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<Reservation>>(okResult.Value);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public async Task GetReservationById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var reservationId = 1;
            var reservation = CreateTestReservation(reservationId);
            _reservationServiceMock.Setup(service => service.GetByIdAsync(reservationId)).ReturnsAsync(reservation);

            // Act
            var result = await _reservationController.GetReservationById(reservationId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Reservation>(okResult.Value);
            Assert.Equal(reservationId, model.Id);
        }

            [Fact]
            public async Task AddReservation_ValidInput_ReturnsCreatedAtActionResult()
            {
                // Arrange
                var newUser = new User { Id = 1, UserRole = Role.ADMIN, Name = "Klaas" };
                var newTrip = new Trip { Id = 100, CarId = 5, DriverId = 1 };
                var newReservation = new Reservation { Id = 1001, TripId = 100, UserId = 1 };

                _userServiceMock.Setup(service => service.ExistsAsync(newUser.Id)).ReturnsAsync(true);
                _tripServiceMock.Setup(service => service.ExistsAsync(newTrip.Id)).ReturnsAsync(true);
                _reservationServiceMock.Setup(service => service.ExistsAsync(newReservation.UserId, newReservation.TripId)).ReturnsAsync(false);
                _reservationServiceMock.Setup(service => service.AddAsync(newReservation)).Returns(Task.CompletedTask);

                // Act
                var result = await _reservationController.AddReservation(newReservation);

                // Assert
                var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

                // Extract the value from the action result and assert its structure and content
                var actualValue = createdAtActionResult.Value;
                Assert.NotNull(actualValue);

                // The returned object is an anonymous type, so we need to use reflection to inspect it
                var properties = actualValue.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(actualValue));
                Assert.True(properties.ContainsKey("message"));
                Assert.True(properties.ContainsKey("reservation"));

                var reservation = properties["reservation"] as Reservation;
                Assert.NotNull(reservation);
                Assert.Equal(newReservation.Id, reservation.Id);
            }
        

        [Fact]
        public async Task UpdateReservation_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var reservationId = 1;
            var updatedReservation = CreateTestReservation(reservationId);
            _reservationServiceMock.Setup(service => service.GetByIdAsync(reservationId)).ReturnsAsync(updatedReservation);
            _reservationServiceMock.Setup(service => service.UpdateAsync(updatedReservation)).Returns(Task.CompletedTask);

            // Act
            var result = await _reservationController.UpdateReservation(reservationId, updatedReservation);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = okResult.Value as string;

            Assert.Equal("Reservation updated successfully", message);
        }

        [Fact]
        public async Task UpdateReservation_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 99;
            Reservation nullReservation = null;
            _reservationServiceMock.Setup(service => service.GetByIdAsync(nonExistingId)).ReturnsAsync(nullReservation);

            // Act
            var result = await _reservationController.UpdateReservation(nonExistingId, new Reservation { Id = nonExistingId });

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteReservation_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var reservationId = 1;
            var reservation = CreateTestReservation(reservationId);
            _reservationServiceMock.Setup(service => service.GetByIdAsync(reservationId)).ReturnsAsync(reservation);
            _reservationServiceMock.Setup(service => service.DeleteAsync(reservationId)).Returns(Task.CompletedTask);

            // Act
            var result = await _reservationController.DeleteReservation(reservationId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            //Assert.Equal("Reservation deleted successfully", okResult.Value);
            var value = okResult.Value;
            Assert.NotNull(value);

            var properties = value.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(value));
            Assert.True(properties.ContainsKey("message"));

            string message = properties["message"] as string;
            Assert.Equal("Reservation deleted successfully", message);
        }

        [Fact]
        public async Task DeleteReservation_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var nonExistingId = 99;
            _reservationServiceMock.Setup(service => service.GetByIdAsync(nonExistingId)).ReturnsAsync((Reservation)null);

            // Act
            var result = await _reservationController.DeleteReservation(nonExistingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
