using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core.Domain;
using Core.DomainService.Interfaces;

namespace RideLinkerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ILogger<TripController> _logger;
        private readonly ITripService _tripService;
        private readonly IReservationService _reservationService;

        public TripController(ILogger<TripController> logger, ITripService tripService, IReservationService reservationService)
        {
            _logger = logger;
            _tripService = tripService;
            _reservationService = reservationService;
        }
        [HttpGet]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetAllTrips()
        {
            _logger.LogInformation("GetAllTrips() aangeroepen");

            try
            {
                var trips = await _tripService.GetAllAsync();
                return Ok(trips);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het ophalen van trips: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het ophalen van trips.");
            }
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetTripById(int id)
        {
            _logger.LogInformation($"GetTripById({id}) aangeroepen");
            var trip = await _tripService.GetByIdAsync(id);

            if (trip == null)
            {
                _logger.LogError($"Trip met id {id} niet gevonden");
                return NotFound();
            }

            return Ok(trip);
        }


        [HttpPost()]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> AddTrip([FromBody] Trip inTrip)
        {
            _logger.LogInformation("AddTrip() aangeroepen");

            try
            {
                await _tripService.AddAsync(inTrip);
                Reservation newReservation = new Reservation
                {
                    UserId = inTrip.DriverId,
                    TripId = inTrip.Id,
                };
                await _reservationService.AddAsync(newReservation);
                _logger.LogInformation("Maak reservation voor aangemaakte trip: " + newReservation);
                return Ok(inTrip);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het toevoegen van een trip: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het toevoegen van een trip.");
            }
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> UpdateTrip(int id, [FromBody] Trip updatedTrip)
        {
            _logger.LogInformation($"UpdateTrip({id}) aangeroepen");

            if (updatedTrip == null)
            {
                return BadRequest();
            }

            updatedTrip.Id = id;

            try
            {
                await _tripService.UpdateAsync(updatedTrip);

                if (updatedTrip == null)
                {
                    return NotFound();
                }

                return Ok(updatedTrip);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het bijwerken van trip: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het bijwerken van een trip.");
            }
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> DeleteTrip(int id)
        {
            _logger.LogInformation($"DeleteTrip({id}) aangeroepen");

            try
            {
                var trip = await _tripService.GetByIdAsync(id);
                if (trip == null)
                {
                    _logger.LogWarning($"Trip met id {id} niet gevonden");
                    return NotFound($"Trip met id {id} niet gevonden");
                }
                await _tripService.DeleteAsync(id);
                return Ok("Trip ID gedelete: " + id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het verwijderen van de trip: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het verwijderen van de trip.");
            }
        }


        [HttpGet("active")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetActiveTrips()
        {
            try
            {
                var activeTrips = await _tripService.GetActiveTripsAsync();
                if (activeTrips.Any())
                {
                    return Ok(activeTrips);
                }
                else
                {
                    _logger.LogWarning("Geen actieve ritten gevonden");
                    return NotFound("Geen actieve ritten gevonden");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fout bij het ophalen van actieve ritten: {ex.Message}");
                return StatusCode(500, "Er is een interne fout opgetreden bij het ophalen van actieve ritten.");
            }
        }

    }
}
