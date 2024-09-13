using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentACar.Application.Interfaces;
using RentACar.Domain;
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
using BCrypt.Net; // Add this to use BCrypt for password hashing

namespace RentACar.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRenterRepository _renterRepository;

        public AuthService(IRenterRepository renterRepository)
        {
            _renterRepository = renterRepository;
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
                Name = user.User.Name,
                Email = user.User.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                LicenseType = user.LicenseType,
                RentCount = user.RentCount,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender
            };
        }

        public async Task<AuthResultDto> LoginAsync(LoginDTO login)
        {
            var user = await _renterRepository.GetRenterByMailAsync(login.Email);

            if (user != null && VerifyPassword(login.Password, user.User.PasswordHashed))
            {
                var token = GenerateToken(login.Email, "renter");
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

        public async Task<bool> RegisterAsync(RegisterDTO addRenter)
        {
            var user = new User
            {
                Name = addRenter.Name,
                Email = addRenter.Email,
                PasswordHashed = HashPassword(addRenter.Password),
                Role = "renter" // For now
            };

            var renter = new Renter
            {
                GovIdNumber = addRenter.GovIdNumber,
                LicenseType = addRenter.LicenseType,
                DateOfBirth = addRenter.DateOfBirth,
                PhoneNumber = addRenter.PhoneNumber,
                Gender = addRenter.Gender,
                Address = addRenter.Address,
                User = user
            };

            await _renterRepository.AddRenterAsync(renter);

            return true;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password); 
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
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
    }
}

