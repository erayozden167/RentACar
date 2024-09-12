using RentACar.DTOs.Auth;
using RentACar.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginAsync(LoginDTO login);
        Task<UserProfileDTO> GetUserProfileAsync(string token);
        Task<bool> RegisterAsync(RegisterDTO addRenter);
    }
}
