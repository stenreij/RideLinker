using Core.Domain;
using Core.DomainService.Interfaces;
using Core.DomainService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RideLinkerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        private readonly ITripService _tripService;

        public ReservationController(ILogger<ReservationController> logger, IReservationService reservationService, IUserService userService, ITripService tripService)
        {
            _logger = logger;
            _reservationService = reservationService;
            _userService = userService;
            _tripService = tripService;
        }

        [HttpGet]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetAllReservations()
        {
            _logger.LogInformation("GetAllReservations called");

            try
            {
                var reservations = await _reservationService.GetAllAsync();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving reservations: {ex.Message}");
                return StatusCode(500, "An internal error occurred while retrieving reservations.");
            }
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> GetReservationById(int id)
        {
            _logger.LogInformation($"GetReservationById({id}) called");

            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                return Ok(reservation);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving reservation with id {id}: {ex.Message}");
                return StatusCode(500, "An internal error occurred while retrieving the reservation.");
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> AddReservation([FromBody] Reservation reservation)
        {
            _logger.LogInformation("Attempting to add a new reservation.");       

            if (reservation == null)
            {
                _logger.LogWarning("AddReservation called with null reservation object.");
                return BadRequest("Reservation data cannot be null.");
            }

            if (reservation.UserId <= 0)
            {
                _logger.LogWarning("AddReservation called with invalid User ID.");
                return BadRequest("A valid User ID is required.");
            }
            if (reservation.TripId <= 0)
            {
                _logger.LogWarning("AddReservation called with invalid Trip ID.");
                return BadRequest("A valid Trip ID is required.");
            }

            bool userExists = await _userService.ExistsAsync(reservation.UserId);
            if (!userExists)
            {
                _logger.LogWarning($"User with ID {reservation.UserId} does not exist.");
                return BadRequest("User ID does not exist.");
            }

            bool tripExists = await _tripService.ExistsAsync(reservation.TripId);
            if (!tripExists)
            {
                _logger.LogWarning($"Trip with ID {reservation.TripId} does not exist.");
                return BadRequest("Trip ID does not exist.");
            }

            bool reservationExists = await _reservationService.ExistsAsync(reservation.UserId, reservation.TripId);
            if (reservationExists)
            {
                _logger.LogWarning($"A reservation with User ID {reservation.UserId} and Trip ID {reservation.TripId} already exists.");
                return Conflict("A reservation with the specified User ID and Trip ID already exists.");
            }

            try
            {
                _logger.LogInformation($"Received reservation data: UserId={reservation.UserId}, TripId={reservation.TripId}");

                await _reservationService.AddAsync(reservation);
                _logger.LogInformation($"Reservation successfully created for User ID {reservation.UserId} and Trip ID {reservation.TripId}.");
                return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, new { message = "Reservation created successfully", reservation });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while adding a reservation: {ex.Message}");
                return StatusCode(500, "An internal error occurred while adding the reservation.");
            }
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> UpdateReservation(int id, [FromBody] Reservation reservation)
        {
            _logger.LogInformation($"UpdateReservation({id}) called");

            try
            {
                if (reservation == null)
                {
                    return BadRequest("Reservation data is invalid");
                }

                var existingReservation = await _reservationService.GetByIdAsync(id);
                if (existingReservation == null)
                {
                    return NotFound($"Reservation with ID {id} not found.");
                }

                reservation.Id = id;
                await _reservationService.UpdateAsync(reservation);
                return Ok("Reservation updated successfully");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating reservation with id {id}: {ex.Message}");
                return StatusCode(500, "An internal error occurred while updating the reservation.");
            }
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(AuthFilter))]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            _logger.LogInformation($"DeleteReservation({id}) called");

            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }

                await _reservationService.DeleteAsync(id);
                return Ok(new { message = "Reservation deleted successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting reservation with id {id}: {ex.Message}");
                return StatusCode(500, "An internal error occurred while deleting the reservation.");
            }
        }
    }
}
