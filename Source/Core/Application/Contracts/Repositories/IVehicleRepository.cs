using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Contracts.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<List<Vehicle>> GetPagedVehicle(int page, int size);

        Task<int> countVehicle();
    }
}