using Core.Domain;
using Core.DomainService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainService.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> GetByIdAsync(int id)
        {
            return await _locationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _locationRepository.GetAllAsync();
        }

        public async Task AddAsync(Location location)
        {
            await _locationRepository.AddAsync(location);
        }

        public async Task UpdateAsync(Location updatedLocation)
        {
            var existingLocation = await _locationRepository.GetByIdAsync(updatedLocation.Id);
            if (existingLocation != null)
            {
                if (!string.IsNullOrEmpty(updatedLocation.Name))
                {
                    existingLocation.Name = updatedLocation.Name;
                }
                if (!string.IsNullOrEmpty(updatedLocation.Address))
                {
                    existingLocation.Address = updatedLocation.Address;
                }
                if (updatedLocation.Cars != null)
                {
                    UpdateCarsCollection(existingLocation.Cars, updatedLocation.Cars);
                }

                await _locationRepository.UpdateAsync(existingLocation);
            }
            else
            {
                throw new ArgumentException("Location with id: " + updatedLocation.Id + " does not exist");
            }
        }

        private void UpdateCarsCollection(ICollection<Car> existingCars, ICollection<Car> updatedCars)
        {
            var existingCarIds = existingCars.Select(c => c.Id).ToHashSet();
            var updatedCarIds = updatedCars.Select(c => c.Id).ToHashSet();

            existingCars.Where(c => !updatedCarIds.Contains(c.Id)).ToList()
                .ForEach(carToRemove => existingCars.Remove(carToRemove));

            foreach (var car in updatedCars.Where(c => !existingCarIds.Contains(c.Id)))
            {
                existingCars.Add(car);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location != null)
            {
                await _locationRepository.DeleteAsync(location);
            }
        }
    }
}
