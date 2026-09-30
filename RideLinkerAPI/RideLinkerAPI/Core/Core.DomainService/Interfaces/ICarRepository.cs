using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainService.Interfaces
{
    public interface ICarRepository
    {
        Task<Car> GetByIdAsync(int id);
        Task<IEnumerable<Car>> GetAllAsync();
        Task AddAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(int id);
    }
}
