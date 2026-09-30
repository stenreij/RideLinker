using Core.Domain;
using Core.DomainService.Interfaces;
using Infrastructure.RL.Migrations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace RideLinkerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ILocationService _locationService;

        public LocationController(ILogger<LocationController> logger, ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetAllLocations()
        {
            _logger.LogInformation("GetAllLocations aangeroepen");

            try
            {
                var locations = await _locationService.GetAllAsync();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het ophalen van locaties {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het ophalen van locaties.");
            }
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetLocationById(int id)
        {
            _logger.LogInformation($"GetLocationById({id}) aangeroepen");
            try
            {
                var location = await _locationService.GetByIdAsync(id);
                if (location == null)
                {
                    return NotFound();
                }
                return Ok(location);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het ophalen van locatie met id {id}: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het ophalen van locatie.");
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> AddLocation([FromBody] Location location)
        {
            _logger.LogInformation("AddLocation aangeroepen");

            try
            {
                if (location == null)
                {
                    return BadRequest("Location data is null");
                }
                await _locationService.AddAsync(location);
                var addedLocation = await _locationService.GetByIdAsync(location.Id);
                return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, addedLocation);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het toevoegen van een locatie: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het toevoegen van een locatie.");
            }
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] Location location)
        {
            _logger.LogInformation($"UpdateLocation({id}) aangeroepen");
            try
            {
                if (location == null)
                {
                    return BadRequest("Location data is invalid");
                }

                var existingLocation = await GetLocationById(id);
                if (existingLocation == null)
                {
                    return NotFound($"Location with ID {id} not found.");
                }
                location.Id = id;
                await _locationService.UpdateAsync(location);
                return Ok(location);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het updaten van locatie met id {id}: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het updaten van locatie.");
            }
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            _logger.LogInformation($"DeleteLocation({id}) aangeroepen");
            try
            {
                var location = await _locationService.GetByIdAsync(id);
                if (location == null)
                {
                    return NotFound();
                }

                await _locationService.DeleteAsync(id);
                return Ok($"Location ID gedelete: {location.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het verwijderen van locatie met id {id}: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het verwijderen van locatie.");
            }
        }
    }
}
