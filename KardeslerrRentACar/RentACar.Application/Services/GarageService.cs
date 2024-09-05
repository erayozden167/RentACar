using RentACar.Application.Interfaces;
using RentACar.DTOs.Garage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services
{
    public class GarageService : IGarageService
    {
        public async Task<GetGarageDTO> AddGarageAsync(AddGarageDTO garage)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteGarageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetGarageDTO> GetGarageAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetGaragesDTO>> GetGaragesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<GetGarageDTO> UpdateGarageAsync()
        {
            throw new NotImplementedException();
        }
    }
}
