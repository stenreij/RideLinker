using Moq;
using Microsoft.Extensions.Logging;
using RideLinkerAPI.Controllers;
using Core.Domain;
using Core.DomainService.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
    public class UserTest
    {
        private readonly UserController _userController;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly Mock<IUserService> _userServiceMock;

        public UserTest()
        {
            _loggerMock = new Mock<ILogger<UserController>>();
            _userServiceMock = new Mock<IUserService>();

            _userController = new UserController(
                _loggerMock.Object,
                _userServiceMock.Object
            );
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResult()
        {
            // Arrange
            var users = new List<User> { new User { Id = 1, Name = "Sten" }, new User { Id = 2, Name = "Kalle" } };
            _userServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _userController.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<User>>(okResult.Value);
            Assert.Equal(2, model.Count);
            Assert.Equal("Sten", users.ElementAt(0).Name);
            Assert.Equal("Kalle", users.ElementAt(1).Name);
        }

        [Fact]
        public async Task GetUserById_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var userId = 1;
            var user = new User { Id = userId, Name = "Andre" };
            _userServiceMock.Setup(service => service.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await _userController.GetUserById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<User>(okResult.Value);
            Assert.Equal(userId, model.Id);
            Assert.Equal("Andre", model.Name);
        }

        [Fact]
        public async Task GetUserById_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = 1;
            _userServiceMock.Setup(service => service.GetByIdAsync(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userController.GetUserById(userId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AddUser_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var inUser = new User { Id = 1, Name = "Andre", UserRole = Role.EMPLOYEE };

            // Act
            var result = await _userController.AddUser(inUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<User>(okResult.Value);
            Assert.Equal("Andre", model.Name);
            Assert.Equal("EMPLOYEE", model.UserRole.ToString());
        }

        [Fact]
        public async Task AddUser_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            var inUser = new User { Id = 1, UserRole = Role.EMPLOYEE }; 

            // Act
            var result = await _userController.AddUser(inUser);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Naam is vereist voor het toevoegen van een gebruiker.", badRequestResult.Value);
        }

        [Fact]
        public async Task UpdateUser_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var userId = 1;
            var updatedUser = new User { Id = userId, Name = "Johan" };

            // Act
            var result = await _userController.UpdateUser(userId, updatedUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<User>(okResult.Value);
            Assert.Equal(userId, model.Id);
            Assert.Equal("Johan", model.Name);
        }

        [Fact]
        public async Task UpdateUser_InvalidInput_ReturnsBadRequestResult()
        {
            // Arrange
            var userId = 1;
            User updatedUser = null;

            // Act
            var result = await _userController.UpdateUser(userId, updatedUser);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ExistingId_ReturnsOkResult()
        {
            // Arrange
            var userId = 2;
            var users = new List<User> { new User { Id = 1, Name = "Andre" }, new User { Id = 2, Name = "Johan" } };
            _userServiceMock.Setup(service => service.GetByIdAsync(userId)).ReturnsAsync(users.FirstOrDefault(u => u.Id == userId));
            _userServiceMock.Setup(service => service.DeleteAsync(userId)).Returns(Task.CompletedTask);

            // Act
            var result = await _userController.DeleteUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal($"User ID gedelete: {userId}", okResult.Value);
        }
        [Fact]
        public async Task DeleteUser_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            var userId = 0;
            _userServiceMock.Setup(service => service.DeleteAsync(userId)).Throws(new Exception("Error deleting user"));

            // Act
            var result = await _userController.DeleteUser(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((NotFoundObjectResult)result).StatusCode);
            Assert.Equal("User met dit Id is niet gevonden.", notFoundResult.Value);

        }
    }
}
