using RentACar.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Infrastructure.Interfaces
{
    public interface IRenterRepository
    {
        Task<List<Renter>> GetRentersAsync();
        Task<Renter?> GetRenterByIdAsync(int id);
        Task<bool> AddRenterAsync(Renter renter);
        Task<bool> UpdateRenterAsync(Renter renter);
        Task<bool> DeleteRenterAsync(int id);
        Task<Renter?> GetRenterByMailAsync(string Email);
    }
}
