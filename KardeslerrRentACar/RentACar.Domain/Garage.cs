using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    public class Garage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GarageName { get; set; } = default!;

        [Required]
        public string Location { get; set; } = default!;

        [Required]
        public DateTime EstablishDate { get; set; }

        [AllowNull]
        public decimal BalanceSheet { get; set; }




        // Relations

        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public ICollection<Renter> Renters { get; set; } = new List<Renter>();

        public ICollection<Employee> Employers { get; set; } = new List<Employee>();




    }
}
