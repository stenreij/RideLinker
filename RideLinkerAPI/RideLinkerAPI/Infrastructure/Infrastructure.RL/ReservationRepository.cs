using Core.Domain;
using Core.DomainService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RL
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly RideLinkerDbContext _context;

        public ReservationRepository(RideLinkerDbContext context)
        {
            _context = context;
        }

        public async Task<Reservation> GetByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Trip)
                .FirstAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Trip)
                .ToListAsync();
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int userId, int tripId)
        {
            return await _context.Reservations
                .AnyAsync(r => r.UserId == userId && r.TripId == tripId);
        }
    }
}
