using RentACar.DTOs.Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Interfaces
{
    public interface IGarageService
    {
        Task<List<GetGaragesDTO>> GetGaragesAsync();
        Task<GetGarageDTO?> GetGarageAsync(int id);
        Task<GetGarageDTO?> AddGarageAsync(AddGarageDTO garage);
        Task<GetGarageDTO?> UpdateGarageAsync(UpdateGarageDTO updateGarage);
        Task<bool> DeleteGarageAsync(int id);
    }
}
