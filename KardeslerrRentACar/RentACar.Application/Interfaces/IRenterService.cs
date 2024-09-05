using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.DTOs.Auth;
using RentACar.DTOs.Renter;

namespace RentACar.Application.Interfaces
{
    public interface IRenterService
    {
        Task<List<GetRentersDTO>> GetRentersAsync();
        Task<GetRenterDTO?> GetRenterAsync(int id);
        Task<GetRenterDTO?> UpdateRenterAsync(UpdateRenterDTO updateRenter);
        Task<bool> DeleteRenterAsync(int id);
    }
}
