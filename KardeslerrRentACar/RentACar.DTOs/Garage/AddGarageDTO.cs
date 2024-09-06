using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.DTOs.Garage
{
    public class AddGarageDTO
    {
        [Required]
        public string GarageName { get; set; } = default!;

        [Required]
        public string Location { get; set; } = default!;

        [Required]
        public DateTime EstablishDate { get; set; }
    }
}
