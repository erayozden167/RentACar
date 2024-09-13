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
    public class RenterRepository : IRenterRepository
    {
        private readonly ApplicationDbContext _context;

        public RenterRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<bool> AddRenterAsync(Renter renter)
        {
            try
            {
                // user ekleme
                await _context.Users.AddAsync(renter.User);

                // renter ekleme
                await _context.Renters.AddAsync(renter);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRenterAsync(int id)
        {
            var renter = await _context.Renters.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
            if (renter == null)
            {
                return false;
            }

            // User tablosundan sil
            _context.Users.Remove(renter.User);
            _context.Renters.Remove(renter);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Renter?> GetRenterByIdAsync(int id)
        {
            return await _context.Renters.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Renter>> GetRentersAsync()
        {
            return await _context.Renters.Include(r => r.User).ToListAsync();
        }

        public async Task<bool> UpdateRenterAsync(Renter renter)
        {
            var renterEntity = await _context.Renters.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == renter.Id);
            if (renterEntity == null)
            {
                return false;
            }

            // Güncelleme işlemleri
            renterEntity.User.Name = renter.User.Name;
            renterEntity.User.Email = renter.User.Email;
            renterEntity.PhoneNumber = renter.PhoneNumber;
            renterEntity.Address = renter.Address;


            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Renter?> GetRenterByMailAsync(string email)
        {
            return await _context.Renters.Include(r => r.User).FirstOrDefaultAsync(x => x.User.Email == email);
        }
        public async Task<bool> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
