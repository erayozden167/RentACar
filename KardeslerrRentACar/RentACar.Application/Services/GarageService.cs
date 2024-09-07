using RentACar.Application.Interfaces;
using RentACar.Domain;
using RentACar.DTOs.Garage;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services
{
    public class GarageService : IGarageService
    {
        private readonly IGarageRepository _garageRepository;
        public GarageService(IGarageRepository garageRepository)
        {
            _garageRepository = garageRepository;
        }

        public async Task<GetGarageDTO?> AddGarageAsync(AddGarageDTO addGarage)
        {
            Garage garage = new Garage()
            {
                GarageName = addGarage.GarageName,
                Address = addGarage.Location,
                EstablishDate = addGarage.EstablishDate
            };
            Garage? response = await _garageRepository.AddGarageAsync(garage);
            if (response == null)
            {
                return null;
            }
            
            return ConvertToGetGarageDTO(response);
        }

        public async Task<bool> DeleteGarageAsync(int id)
        {
            bool response = await _garageRepository.DeleteGarageAsync(id);
            if (!response)
            {
                return false;
            }
            return true;
        }

        public async Task<GetGarageDTO?> GetGarageAsync(int id)
        {
            Garage? garage = await _garageRepository.GetGarageAsync(id);
            if (garage == null)
            {
                return null;
            }
            return ConvertToGetGarageDTO(garage);
        }

        public async Task<List<GetGaragesDTO>> GetGaragesAsync()
        {
            List<Garage> garages = await _garageRepository.GetGaragesAsync();
            return garages.Select(x => new GetGaragesDTO
            {
                Id = x.Id,
                GarageName = x.GarageName,
                Location = x.Address
            }).ToList();
        }

        public async Task<GetGarageDTO?> UpdateGarageAsync(UpdateGarageDTO updateGarage)
        {
            Garage garage = new Garage()
            {
                Id = updateGarage.Id,
                GarageName = updateGarage.Name,
                Address = updateGarage.Location
            };
            Garage? response = await _garageRepository.UpdateGarageAsync(garage);
            if (response == null)
            {
                return null;
            }
            return ConvertToGetGarageDTO(response);
        }
        private GetGarageDTO ConvertToGetGarageDTO(Garage garage)
        {
            return new GetGarageDTO()
            {
                Id = garage.Id,
                GarageName = garage.GarageName,
                Location = garage.Address,
                EstablishDate = garage.EstablishDate,
                BalanceSheet = (decimal)garage.BalanceSheet
            };
        }
    }
}
