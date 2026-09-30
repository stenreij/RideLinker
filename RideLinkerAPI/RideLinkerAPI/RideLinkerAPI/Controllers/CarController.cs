using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core.Domain;
using Core.DomainService.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace RideLinkerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase 
    {
        private readonly ILogger<CarController> _logger;
        private readonly ICarService _carService;
        private readonly ILocationService _locationService;

        public CarController(ILogger<CarController> logger, ICarService carService, ILocationService locationService)
        {
            _logger = logger;
            _carService = carService;
            _locationService = locationService;
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetAllCars()
        {
            _logger.LogInformation("GetAllCars() aangeroepen");

            try
            {
                var cars = await _carService.GetAllAsync();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het ophalen van auto's: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het ophalen van auto's.");
            }
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetCarById(int id)
        {
            _logger.LogInformation($"GetCarById({id}) aangeroepen");
            var car = await _carService.GetByIdAsync(id);

            if (car == null)
            {
                _logger.LogError($"Auto met id {id} niet gevonden");
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost()]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> AddCar([FromBody] Car inCar)
        {
            _logger.LogInformation("AddCar() aangeroepen");
            _logger.LogInformation($"Car: {inCar.Image}");
            _logger.LogInformation($"LocationId van Car: {inCar.LocationId}");

            try
            {
                Location location = await _locationService.GetByIdAsync(inCar.LocationId);
                _logger.LogInformation($"Location: {location.Name}");

                if (location == null)
                {
                       return BadRequest("Locatie bestaat niet");
                }
                if (string.IsNullOrEmpty(inCar.Brand))
                {
                    return BadRequest("Merk (Brand) is vereist voor het toevoegen van een auto.");
                }

                inCar.Location = location;
                await _carService.AddAsync(inCar);
                return Ok(inCar);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het toevoegen van een auto: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het toevoegen van een auto.");
            }
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car updatedCar)
        {
            _logger.LogInformation($"UpdateCar({id}) aangeroepen");

            if (updatedCar == null)
            {
                return BadRequest();
            }

            updatedCar.Id = id;

            try
            {
                 await _carService.UpdateAsync(updatedCar);

                if (updatedCar == null)
                {
                    return NotFound();
                }

                return Ok(updatedCar);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het bijwerken van auto: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het bijwerken van een auto.");
            }
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> DeleteCar(int id)
        {
            _logger.LogInformation($"DeleteCar({id}) aangeroepen");

            try
            {
                var car = await _carService.GetByIdAsync(id);
                if (car == null)
                {
                    _logger.LogWarning($"Auto met id {id} niet gevonden");
                    return NotFound("Auto met dit Id is niet gevonden."); 
                }

                await _carService.DeleteAsync(id);
                return Ok("Auto ID gedelete: " + id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het verwijderen van de auto: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het verwijderen van de auto.");
            }
        }
    }
}
