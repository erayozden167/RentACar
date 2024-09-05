using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Domain;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _context;
        public VehicleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Vehicle?> AddVehicleAsync(Vehicle vehicle)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Garage? garage = await _context.Garages.FindAsync(vehicle.GarageId);
                if (garage == null)
                {
                    return null;
                }
                garage.Vehicles.Add(vehicle);
                vehicle.Garage = garage;
                await _context.Vehicles.AddAsync(vehicle);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return vehicle;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<bool> DeleteVehicleAsync(int vehicleId)
        {
            Vehicle? vehicle = await _context.Vehicles.FindAsync(vehicleId);

            if (vehicle == null)
            {
                return false;
            }

           _context.Vehicles.Remove(vehicle);
           await _context.SaveChangesAsync();
            return true;
         
        }

        public async Task<Vehicle?> GetVehicleDetailsAsync(int vehicleId)
        {
            return await _context.Vehicles.FindAsync(vehicleId);
        }

        public async Task<List<Vehicle>?> GetVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<Vehicle?> UpdateVehicleAsync(Vehicle vehicle)
        {
            Vehicle? entity = await _context.Vehicles.FindAsync(vehicle.Id);
            if (entity == null) 
            {
                return null;
            }
            entity.Name = vehicle.Name;
            entity.LicensePlate = vehicle.LicensePlate;
            entity.Color = vehicle.Color;
            entity.Kms = vehicle.Kms; 
            entity.RentalPrice = vehicle.RentalPrice;
            entity.Hp = vehicle.Hp;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
