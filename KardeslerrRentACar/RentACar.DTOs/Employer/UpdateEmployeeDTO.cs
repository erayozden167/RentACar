using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Employer
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }
        [Required]
        [StringLength(111, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]       
        public string Role { get; set; } = string.Empty;        

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,24}$")]
        public string Password { get; set; } = string.Empty;
    }
}
