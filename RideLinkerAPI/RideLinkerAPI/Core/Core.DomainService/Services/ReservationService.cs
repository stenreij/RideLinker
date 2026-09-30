using Core.Domain;
using Core.DomainService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainService.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _reservationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _reservationRepository.GetAllAsync();
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _reservationRepository.AddAsync(reservation);
        }

        public async Task UpdateAsync(Reservation updatedReservation)
        {
            var existingReservation = await _reservationRepository.GetByIdAsync(updatedReservation.Id);
            if (existingReservation != null)
            {
                if (updatedReservation.UserId != 0)
                {
                    existingReservation.UserId = updatedReservation.UserId;
                }
                if (updatedReservation.TripId != 0)
                {
                    existingReservation.TripId = updatedReservation.TripId;
                }
                await _reservationRepository.UpdateAsync(existingReservation);
            }
            else
            {
                throw new ArgumentException("Reservation with id: " + updatedReservation.Id + " does not exist");
            }
        }


        public async Task DeleteAsync(int id)
        {
            var reservation = await GetByIdAsync(id);
            if (reservation != null)
            {
                await _reservationRepository.DeleteAsync(reservation);
            }
        }

        public async Task<bool> ExistsAsync(int userId, int tripId)
        {
            return await _reservationRepository.ExistsAsync(userId, tripId);
        }
    }
}
