using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Application.Interfaces;
using RentACar.DTOs.Auth;
using RentACar.DTOs.User;
using RentACar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRenterRepository _renterRepository;
        public AuthService(IRenterRepository renterRepository)
        {
            _renterRepository = renterRepository;
        }

        public async Task<AuthResultDto> LoginAsync(LoginDTO login)
        {
            if (await IsValidUser(login.Email, login.Password))
            {
                var token = GenerateToken(login.Email,"renter"); //for now
                var refreshToken = GenerateRefreshToken();

                var result = new AuthResultDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Mail = login.Email,
                    Expiration = DateTime.Now.AddMinutes(30),
                    IsSuccess = true
                };

                return result;
            }
            else
            {
                return new AuthResultDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Kullanıcı adı veya şifre hatalı!"
                };
            }
        }
        public async Task<UserProfileDTO?> GetUserProfileAsync(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var email = jwtToken.Claims.First(claim => claim.Type == JwtRegisteredClaimNames.Sub).Value;

            var user = await _renterRepository.GetRenterByMailAsync(email);
            if (user == null)
            {
                return null;
            }

            return new UserProfileDTO
            {
                //Fill this place
            };
        }
        public async Task<bool> RegisterAsync(RegisterDTO addRenter)
        {
            throw new NotImplementedException();
        }
        private string GenerateToken(string username, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("asdlqwdASFqefqlmsfqwdqQWDASdqw_124134asdlasdQWFQDwasdxczc"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "arkabahcemiz.com.tr",
                audience: "arkabahcemiz.com.tr",
                claims: new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role)
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private async Task<bool > IsValidUser(string username, string password)
        {
            await _renterRepository.GetRentersAsync();
            return true; // Şimdilik her kullanıcı geçerli
        }
    }
}
