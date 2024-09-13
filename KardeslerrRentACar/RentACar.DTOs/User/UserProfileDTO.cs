using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.User
{
    public class UserProfileDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string LicenseType { get; set; } = string.Empty;
        public int RentCount { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }

}
