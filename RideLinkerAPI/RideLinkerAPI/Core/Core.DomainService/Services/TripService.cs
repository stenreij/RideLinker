using Core.Domain;
using Core.DomainService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainService.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _repo;

        public TripService(ITripRepository repo)
        {
            _repo = repo;
        }


        public Task<IEnumerable<Trip>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }

        public Task<Trip> GetByIdAsync(int id)
        {
            return _repo.GetByIdAsync(id);
        }
        public Task AddAsync(Trip trip)
        {
            return _repo.AddAsync(trip);
        }

        public Task UpdateAsync(Trip trip)
        {
            return _repo.UpdateAsync(trip);
        }

        public Task DeleteAsync(int id)
        {
            return _repo.DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(int tripId)
        {
            return await _repo.ExistsAsync(tripId);
        }

        public async Task<IEnumerable<Trip>> GetActiveTripsAsync()
        {
            return await _repo.GetActiveTripsAsync();
        }
    }
}
