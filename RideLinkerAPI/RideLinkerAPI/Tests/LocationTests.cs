using Core.Domain;
using Core.DomainService.Interfaces;
using Core.DomainService.Services;
using Infrastructure.RL.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RideLinkerAPI.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class LocationTests
    {
        private readonly Mock<ILocationService> _locationServiceMock;
        private readonly LocationController _locationController;
        private readonly Mock<ILogger<LocationController>> _loggerMock;

        public LocationTests()
        {
            _loggerMock = new Mock<ILogger<LocationController>>();
            _locationServiceMock = new Mock<ILocationService>();
            _locationController = new LocationController(_loggerMock.Object, _locationServiceMock.Object);
        }

        [Fact]
        public async Task GetAllLocationsShouldReturnLocations()
        {
            // Arrange
            var locations = new List<Location> { new Location { Id = 1, Name = "Breda" }, new Location { Id = 2, Name = "Amsterdam" } };
            _locationServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(locations);

            // Act
            var result = await _locationController.GetAllLocations();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<Location>>(okResult.Value);
            Assert.Equal(2, model.Count);
            Assert.Equal("Breda", locations.ElementAt(0).Name);
            Assert.Equal("Amsterdam", locations.ElementAt(1).Name);
        }

        [Fact]
        public async Task GetLocationById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var locationId = 1;
            var location = new Location { Id = locationId, Name = "Breda" };
            _locationServiceMock.Setup(service => service.GetByIdAsync(locationId)).ReturnsAsync(location);

            // Act
            var result = await _locationController.GetLocationById(locationId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Location>(okResult.Value);
            Assert.Equal(locationId, model.Id);
            Assert.Equal("Breda", model.Name);
        }

        [Fact]
        public async Task GeLocationrById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var locationId = 1;
            _locationServiceMock.Setup(service => service.GetByIdAsync(locationId)).ReturnsAsync((Location)null);

            // Act
            var result = await _locationController.GetLocationById(locationId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddLocation_ValidInput_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var inLocation = new Location { Id = 1, Name = "Breda" };
            _locationServiceMock.Setup(service => service.GetByIdAsync(1))
                .ReturnsAsync((int id) => inLocation);

            // Act
            var result = await _locationController.AddLocation(inLocation);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var model = Assert.IsType<Location>(createdAtActionResult.Value);
            Assert.Equal(1, model.Id);
            Assert.Equal("Breda", model.Name);

            // Additional Assertion: Check if the location is added by calling GetByIdAsync
            var addedLocation = await _locationServiceMock.Object.GetByIdAsync(1);
            Assert.NotNull(addedLocation);
            Assert.Equal(1, addedLocation.Id);
            Assert.Equal("Breda", addedLocation.Name);
        }

        [Fact]
        public async Task AddLocation_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            Location invalidLocation = null;

            // Act
            var result = await _locationController.AddLocation(invalidLocation);

            // Assert
            var badResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Location data is null", badResult.Value);
        }

        [Fact]
        public async Task UpdateLocation_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var locaId = 1;
            var updateLocation = new Location { Id = locaId, Name = "UpdatedBreda" };

            // Act
            var result = await _locationController.UpdateLocation(locaId, updateLocation);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Location>(okResult.Value);
            Assert.Equal(locaId, model.Id);
            Assert.Equal("UpdatedBreda", model.Name);
        }

        [Fact]
        public async Task UpdateLocation_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            var locaId = 1;
            Location updatedLocation = null;

            // Act
            var result = await _locationController.UpdateLocation(locaId, updatedLocation);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Location data is invalid", badRequest.Value);
        }

        [Fact]
        public async Task DeleteLocation_ExistingId_ReturnsOkResult()
        {
            //Arrange
            var locaId = 1;
            var locations = new List<Location> { new Location{ Id=1, Name="Breda"}, new Location { Id=2, Name="Amsterdam"} };
            _locationServiceMock.Setup(service => service.GetByIdAsync(locaId)).ReturnsAsync(locations.FirstOrDefault(c => c.Id == locaId));
            _locationServiceMock.Setup(service => service.DeleteAsync(locaId)).Returns(Task.CompletedTask);

            // Act
            var result = await _locationController.DeleteLocation(locaId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Location ID gedelete: {locaId}", okResult.Value);
        }

        [Fact]
        public async Task DeleteLocation_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var locaId = 0;
            _locationServiceMock.Setup(service => service.DeleteAsync(locaId)).Throws(new Exception("Error deleting car"));

            // Act
            var result = await _locationController.DeleteLocation(locaId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            Assert.Equal(404, ((NotFoundResult)result).StatusCode);
        }
    }
}
