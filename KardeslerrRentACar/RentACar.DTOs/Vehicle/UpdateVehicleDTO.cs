using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Vehicle
{
    public class UpdateVehicleDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [RegularExpression("^[0-9]{2}[A-Z]{1,3}[0-9]{2,4}.{8}$", ErrorMessage = "Invalid license plate format.")]       

        [Required]        
        public string LicensePlate { get; set; } = string.Empty;

        [Required]
        public string Color { get; set; } = string.Empty;
        
        public double? Kms { get; set; }

        [Required]
        public double RentalPrice { get; set; }

        public int? Hp { get; set; }

    }
}
