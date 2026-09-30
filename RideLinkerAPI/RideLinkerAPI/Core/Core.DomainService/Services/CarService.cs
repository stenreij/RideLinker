using Core.Domain;
using Core.DomainService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainService.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repo;
        
        public CarService(ICarRepository repo)
        {
            _repo = repo;
        }

        public Task<IEnumerable<Car>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }

        public Task<Car> GetByIdAsync(int id)
        {
           return _repo.GetByIdAsync(id);
        }
        public Task AddAsync(Car car)
        {
            return _repo.AddAsync(car);
        }

        public Task UpdateAsync(Car car)
        {
            return _repo.UpdateAsync(car);
        }

        public Task DeleteAsync(int id)
        {
            return _repo.DeleteAsync(id);
        }
    }
}
