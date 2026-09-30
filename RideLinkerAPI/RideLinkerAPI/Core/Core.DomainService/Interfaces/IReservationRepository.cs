using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainService.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation> GetByIdAsync(int id);
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task AddAsync(Reservation reservation);
        Task UpdateAsync(Reservation reservation);
        Task DeleteAsync(Reservation reservation);
        Task<bool> ExistsAsync(int userId, int tripId);

    }
}
