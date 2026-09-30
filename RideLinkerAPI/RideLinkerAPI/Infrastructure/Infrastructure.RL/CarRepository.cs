using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Core.DomainService.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.RL
{
    public class CarRepository : ICarRepository
    {
        private readonly RideLinkerDbContext _context;
        private readonly ILogger<CarRepository> _logger;

        public CarRepository(RideLinkerDbContext context, ILogger<CarRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Car> GetByIdAsync(int carId)
        {
            return await _context.Cars
                .Include(c => c.Location)
                //.Include(c => c.Trips)
                //.ThenInclude(t => t.Departure)
                //.Include(c => c.Trips)
                //.ThenInclude(t => t.Destination)
                .FirstAsync(c => c.Id == carId);
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Cars
                .Include(c => c.Location)
                .Include(c => c.Trips)  
                .ThenInclude(t => t.Departure)  
                .Include(c => c.Trips)
                .ThenInclude(t => t.Destination)  
                .ToListAsync();
        }

        public async Task AddAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car car)
        {
            var carToUpdate = await _context.Cars.FindAsync(car.Id);

            _logger.LogInformation("Updating car with id: " + car.Id);

            if (carToUpdate != null)
            {
                _context.Entry(carToUpdate).CurrentValues.SetValues(car);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var carToDelete = await _context.Cars.FindAsync(id);

            if (carToDelete != null)
            {
                _context.Cars.Remove(carToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
