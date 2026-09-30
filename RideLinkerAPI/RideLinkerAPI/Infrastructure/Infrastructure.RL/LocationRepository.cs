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
    public class LocationRepository : ILocationRepository
    {
        private readonly RideLinkerDbContext _context;

        public LocationRepository(RideLinkerDbContext context)
        {
            _context = context;
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations.ToListAsync();
        }

        public async Task AddAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Location location)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }
    }
}
