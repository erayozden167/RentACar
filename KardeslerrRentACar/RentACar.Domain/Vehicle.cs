using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Status { get; set; } = "Available";

        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string Brand { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string FuelType { get; set; }
        
        public int? Hp { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public int Year { get; set; }

        public double? Kms { get; set; }

        [Required]
        public double RentalPrice { get; set; }

        public DateTime DateForRenting { get; set; }

        public int GarageId { get; set; }

        // Relations
        public ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
        public Garage Garage { get; set; } = new Garage();



    }
}
