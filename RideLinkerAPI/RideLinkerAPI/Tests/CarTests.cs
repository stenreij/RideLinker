using Moq;
using Microsoft.Extensions.Logging;
using RideLinkerAPI.Controllers;
using Core.Domain;
using Core.DomainService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class CarTests
    {
        private readonly CarController _carController;
        private readonly Mock<ILogger<CarController>> _loggerMock;
        private readonly Mock<ICarService> _carServiceMock;
        private readonly Mock<ILocationService> _locationServiceMock;

        public CarTests()
        {
            _loggerMock = new Mock<ILogger<CarController>>();
            _carServiceMock = new Mock<ICarService>();
            _locationServiceMock = new Mock<ILocationService>();

            _carController = new CarController(
                _loggerMock.Object,
                _carServiceMock.Object,
                _locationServiceMock.Object
            );
        }

        [Fact]
        public async Task GetAllCars_ReturnsOkResult()
        {
            // Arrange
            var cars = new List<Car> { new Car { Id = 1, Brand = "Audi" }, new Car { Id = 2, Brand = "BMW" } };
            _carServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(cars);

            // Act
            var result = await _carController.GetAllCars();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<Car>>(okResult.Value);
            Assert.Equal(2, model.Count);
            Assert.Equal("Audi", cars.ElementAt(0).Brand);
            Assert.Equal("BMW", cars.ElementAt(1).Brand);
        }

        [Fact]
        public async Task GetCarById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var carId = 1;
            var car = new Car { Id = carId, Brand = "Audi" };
            _carServiceMock.Setup(service => service.GetByIdAsync(carId)).ReturnsAsync(car);

            // Act
            var result = await _carController.GetCarById(carId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Car>(okResult.Value);
            Assert.Equal(carId, model.Id);
            Assert.Equal("Audi", model.Brand);
        }

        [Fact]
        public async Task GetCarById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var carId = 1;
            _carServiceMock.Setup(service => service.GetByIdAsync(carId)).ReturnsAsync((Car)null);

            // Act
            var result = await _carController.GetCarById(carId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddCar_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var inCar = new Car { Id = 1, Brand = "Audi", LocationId = 1 };
            var location = new Location { Id = 1, Name = "Location1" };
            _locationServiceMock.Setup(service => service.GetByIdAsync(inCar.LocationId)).ReturnsAsync(location);

            // Act
            var result = await _carController.AddCar(inCar);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Car>(okResult.Value);
            Assert.Equal("Audi", model.Brand);
            Assert.Equal(1, location.Id);
            Assert.Equal(location, model.Location);
        }
        [Fact]
        public async Task AddInvalidCar_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var inCar = new Car { Id = 1, LocationId = 1 };
            var location = new Location { Id = 1, Name = "Location1" };
            _locationServiceMock.Setup(service => service.GetByIdAsync(inCar.LocationId)).ReturnsAsync(location);

            // Act
            var result = await _carController.AddCar(inCar);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Merk (Brand) is vereist voor het toevoegen van een auto.", badRequestResult.Value);

        }

        [Fact]
        public async Task UpdateCar_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var carId = 1;
            var updatedCar = new Car { Id = carId, Brand = "UpdatedAudi" };

            // Act
            var result = await _carController.UpdateCar(carId, updatedCar);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Car>(okResult.Value);
            Assert.Equal(carId, model.Id);
            Assert.Equal("UpdatedAudi", model.Brand);
        }

        [Fact]
        public async Task UpdateCar_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            var carId = 1;
            Car updatedCar = null; 

            // Act
            var result = await _carController.UpdateCar(carId, updatedCar);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteCar_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var carId = 2;
            var cars = new List<Car> { new Car { Id = 1, Brand = "Audi" }, new Car { Id = 2, Brand = "BMW" } };
            _carServiceMock.Setup(service => service.GetByIdAsync(carId)).ReturnsAsync(cars.FirstOrDefault(c => c.Id == carId));
            _carServiceMock.Setup(service => service.DeleteAsync(carId)).Returns(Task.CompletedTask);

            // Act
            var result = await _carController.DeleteCar(carId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"Auto ID gedelete: {carId}", okResult.Value);
        }

        [Fact]
        public async Task DeleteCar_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var carId = 0;
            _carServiceMock.Setup(service => service.DeleteAsync(carId)).Throws(new Exception("Error deleting car"));

            // Act
            var result = await _carController.DeleteCar(carId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
            Assert.Equal("Auto met dit Id is niet gevonden.", notFoundResult.Value);
        }

    }
}