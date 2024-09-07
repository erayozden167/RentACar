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
    public class GarageRepository : IGarageRepository
    {
        private readonly ApplicationDbContext _context;
        public GarageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Garage?> AddGarageAsync(Garage garage)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _context.AddAsync(garage);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return garage;
            }
            catch
            {
                await transaction.RollbackAsync();
                return null;
            }
            
        }

        public async Task<bool> DeleteGarageAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Garage? garage = await _context.Garages.FindAsync(id);
                if (garage == null)
                {
                    return false;
                }
                _context.Garages.Remove(garage);
                await transaction.CommitAsync();
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<Garage?> GetGarageAsync(int id)
        {
            return await _context.Garages.FindAsync(id);
        }

        public async Task<List<Garage>> GetGaragesAsync()
        {
            return await _context.Garages.ToListAsync();
        }

        public async Task<Garage?> UpdateGarageAsync(Garage updateGarage)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Garage? garage = await _context.Garages.FindAsync(updateGarage.Id);
                if (garage == null)
                {
                    return null;
                }
                garage.GarageName = updateGarage.GarageName;
                garage.Address = updateGarage.Address;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return garage;
            }
            catch 
            {
                await transaction.RollbackAsync();
                return null;
            }
        }
    }
}
