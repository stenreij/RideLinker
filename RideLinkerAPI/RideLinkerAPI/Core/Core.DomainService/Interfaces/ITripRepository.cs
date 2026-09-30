using Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainService.Interfaces
{
    public interface ITripRepository
    {
        Task<Trip> GetByIdAsync(int id);
        Task<IEnumerable<Trip>> GetAllAsync();
        Task AddAsync(Trip trip);
        Task UpdateAsync(Trip trip);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int tripId);
        Task<IEnumerable<Trip>> GetActiveTripsAsync();
    }
}
