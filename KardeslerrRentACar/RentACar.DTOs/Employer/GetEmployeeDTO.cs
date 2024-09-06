using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Employer
{
    public class GetEmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string GovIdNumber { get; set; } = string.Empty;

        public string LicenseType { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string? Gender { get; set; }

        public string Address { get; set; } = string.Empty;


    }
}
