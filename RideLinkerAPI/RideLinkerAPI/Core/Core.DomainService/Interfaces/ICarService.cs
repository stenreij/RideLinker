using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DomainService.Interfaces
{
    public interface ICarService
    {
        Task<Car> GetByIdAsync(int id);
        Task<IEnumerable<Car>> GetAllAsync();
        Task AddAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(int id);
    }
}
