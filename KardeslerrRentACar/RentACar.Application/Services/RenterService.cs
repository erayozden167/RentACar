using Microsoft.AspNetCore.Identity;
using RentACar.Application.Interfaces;
using RentACar.Domain;
using RentACar.DTOs.Renter;
using RentACar.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Application.Services
{
    public class RenterService : IRenterService
    {
        private readonly IRenterRepository _renterRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public RenterService(IRenterRepository renterRepository, IPasswordHasher<User> passwordHasher)
        {
            _renterRepository = renterRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> DeleteRenterAsync(int id)
        {
            return await _renterRepository.DeleteRenterAsync(id);
        }

        public async Task<GetRenterDTO?> GetRenterAsync(int id)
        {
            Renter? renter = await _renterRepository.GetRenterByIdAsync(id);
            if (renter == null)
            {
                return null;
            }

            return ConvertToGetRenterDTO(renter);
        }

        public async Task<List<GetRentersDTO>> GetRentersAsync()
        {
            List<Renter> renters = await _renterRepository.GetRentersAsync();
            if (renters == null || !renters.Any())
            {
                return new List<GetRentersDTO>();
            }

            return renters.Select(r => new GetRentersDTO
            {
                Id = r.Id,
                Name = r.User.Name,
                Email = r.User.Email,
                Phone = r.PhoneNumber
            }).ToList();
        }

        public async Task<GetRenterDTO?> UpdateRenterAsync(UpdateRenterDTO updateRenter)
        {
            Renter? renter = await _renterRepository.GetRenterByIdAsync(updateRenter.Id);
            if (renter == null || _passwordHasher.VerifyHashedPassword(renter.User, renter.User.PasswordHashed, updateRenter.Password) != PasswordVerificationResult.Success)
            {
                return null;
            }

            // Güncelleme işlemleri
            renter.User.Name = updateRenter.Name;
            renter.User.Email = updateRenter.Email;
            renter.PhoneNumber = updateRenter.PhoneNumber;
            renter.Address = updateRenter.Address;

            bool isUpdated = await _renterRepository.UpdateRenterAsync(renter);
            if (!isUpdated)
            {
                return null;
            }

            return ConvertToGetRenterDTO(renter);
        }

        private GetRenterDTO ConvertToGetRenterDTO(Renter renter)
        {
            return new GetRenterDTO
            {
                Id = renter.Id,
                GovIdNumber = renter.GovIdNumber,
                LicenseType = renter.LicenseType,
                DateOfBirth = renter.DateOfBirth,
                Name = renter.User.Name,
                Email = renter.User.Email,
                PhoneNumber = renter.PhoneNumber,
                Gender = renter.Gender,
                Address = renter.Address
            };
        }
    }
}
