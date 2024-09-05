using RentACar.Core.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Renter
{
    public class GetRenterDTO
    {
        public int Id { get; set; }
        public string GovIdNumber { get; set; } = string.Empty;

        public string LicenseType { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? Gender { get; set; }

        public string Address { get; set; } = string.Empty;
    }
}
