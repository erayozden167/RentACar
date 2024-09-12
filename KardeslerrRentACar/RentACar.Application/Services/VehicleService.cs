using RentACar.Application.Interfaces;
using RentACar.Domain;
using RentACar.DTOs.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace RentACar.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository) { _vehicleRepository = vehicleRepository; }

        public async Task<VehicleDTO?> AddVehicleAsync(VehicleDTO vehicleDTO)
        {
            Vehicle vehicle = new Vehicle()
            {
                Name = vehicleDTO.Name,
                LicensePlate = vehicleDTO.LicensePlate,
                Brand = vehicleDTO.Brand,
                Color = vehicleDTO.Color,
                FuelType = vehicleDTO.FuelType,
                Hp = vehicleDTO.Hp,
                VehicleType = vehicleDTO.VehicleType,
                Year = vehicleDTO.Year,
                Kms = vehicleDTO.Kms,
                RentalPrice = vehicleDTO.RentalPrice,
                Status = "Available"
            };

            Vehicle? response = await _vehicleRepository.AddVehicleAsync(vehicle);

            if (response == null)
            {
                return null;
            }
            vehicleDTO.Id = response.Id;

            return vehicleDTO;

        }

        public async Task<bool> DeleteVehicleAsync(int vehicleId)
        {
            bool response = await _vehicleRepository.DeleteVehicleAsync(vehicleId);

            if (!response) { return false; }
            return true;
        }

        public async Task<VehicleDTO?> GetVehicleDetailsAsync(int vehicleId)
        {
            Vehicle? vehicle = await _vehicleRepository.GetVehicleDetailsAsync(vehicleId);

            if (vehicle == null)
            {
                return null;
            }

            VehicleDTO vehicleDTO = new VehicleDTO()
            {
                Id = vehicleId,
                Name = vehicle.Name,
                LicensePlate = vehicle.LicensePlate,
                Brand = vehicle.Brand,
                Color = vehicle.Color,
                FuelType = vehicle.FuelType,
                Hp = vehicle.Hp,
                VehicleType = vehicle.VehicleType,
                Year = vehicle.Year,
                Kms = vehicle.Kms,
                RentalPrice = vehicle.RentalPrice,
                GarageName = vehicle.Garage.GarageName,
                GarageId = vehicle.GarageId
            };
            return vehicleDTO;
        }

        public async Task<List<ListVehicleDTO>> GetVehiclesAsync()
        {
            List<Vehicle>? vehicles = await _vehicleRepository.GetVehiclesAsync();

            if (vehicles == null)
            {
                return new List<ListVehicleDTO>();
            }

            List<ListVehicleDTO> list = new List<ListVehicleDTO>();

            foreach (var vehicle in vehicles)
            {
                ListVehicleDTO vehicleDTO = new ListVehicleDTO()
                {
                    Id = vehicle.Id,
                    Name = vehicle.Name,
                    RentalPrice = vehicle.RentalPrice
                };
                list.Add(vehicleDTO);
            }

            return list;
        }

        public async Task<VehicleDTO?> UpdateVehicleAsync(int vehicleId, UpdateVehicleDTO vehicle)
        {
            Vehicle existing = new Vehicle()
            {
                Id = vehicleId,
                Name = vehicle.Name,
                LicensePlate = vehicle.LicensePlate,
                Color = vehicle.Color,
                Kms = vehicle.Kms,
                RentalPrice = vehicle.RentalPrice,
                Hp = vehicle.Hp
            };
            Vehicle? response = await _vehicleRepository.UpdateVehicleAsync(existing);

            if (response == null)
            {
                return null;
            }

            VehicleDTO vehicleDTO = new VehicleDTO()
            {
                Id = response.Id,
                Name = response.Name,
                LicensePlate = response.LicensePlate,
                Brand = response.Brand,
                Color = response.Color,
                FuelType = response.FuelType,
                Hp = response.Hp,
                VehicleType = response.VehicleType,
                Year = response.Year,
                Kms = response.Kms,
                RentalPrice = response.RentalPrice
            };
            return vehicleDTO;
        }
    }
}
