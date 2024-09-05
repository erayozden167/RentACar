using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Core.Validations;

namespace RentACar.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required]
        [RegularExpression(@"^\d{11}$")]
        public string GovIdNumber { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^[A-Z][0-9]$")]
        public string LicenseType { get; set; } = string.Empty;

        [Required]
        [MinimumAge(21)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(111, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,24}$")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Gender { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Address { get; set; } = string.Empty;
    }
}
