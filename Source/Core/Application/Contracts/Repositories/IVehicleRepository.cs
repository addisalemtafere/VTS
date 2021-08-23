using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Contracts.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<List<Vehicle>> GetPagedVehicle(int page, int size);
        Task<int> countVehicle();
    }
}