using System;
using RentACar.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>?> GetVehiclesAsync();
        
        Task<Vehicle?> GetVehicleDetailsAsync(int vehicleId);

        Task<Vehicle?> AddVehicleAsync(Vehicle vehicle);

        Task<bool> DeleteVehicleAsync(int vehicleId);

        Task<Vehicle?> UpdateVehicleAsync(Vehicle vehicle);
        

    }
}
