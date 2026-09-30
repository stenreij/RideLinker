using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.DomainService.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetByIdAsync(int id);
        Task<IEnumerable<Location>> GetAllAsync();
        Task AddAsync(Location location);
        Task UpdateAsync(Location location);
        Task DeleteAsync(int id);
    }
}
