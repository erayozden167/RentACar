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

        public double RentalPrice { get; set; }

    }
}
