using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace RentACar.DTOs.Vehicle
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[0-9]{2}[A-Z]{1,2}[0-9]{2,4}$|^[0-9]{2}[A-Z]{3}[0-9]{2,3}$", ErrorMessage = "Invalid license plate format.")]
        public string LicensePlate { get; set; } = null!;
        [Required]
        public string Brand { get; set; } = null!;
        [Required]
        public string Color { get; set; } = null!;
        [Required]
        public string FuelType { get; set; } = null!;
        
        public int? Hp { get; set; }

        [Required]
        public string VehicleType { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        public double? Kms { get; set; }

        [Required]
        public double RentalPrice { get; set; }
        public int GarageId { get; set; }
        public string? GarageName { get; set; }
    }
}
