using RentACar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Interfaces
{
    public interface IGarageRepository
    {
        Task<List<Garage>> GetGaragesAsync();
        Task<Garage?> GetGarageAsync(int id);
        Task<Garage?> AddGarageAsync(Garage garage);
        Task<Garage?> UpdateGarageAsync(Garage updateGarage);
        Task<bool> DeleteGarageAsync(int id);
    }
}
