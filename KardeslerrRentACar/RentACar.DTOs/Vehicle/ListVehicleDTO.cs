using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Vehicle
{
    public class ListVehicleDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Brand { get; set; }
        public string? VehicleType { get; set; }

        public double RentalPrice { get; set; }

    }
}
