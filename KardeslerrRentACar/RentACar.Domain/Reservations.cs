using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentACar.Core.Interfaces;
using RentACar.Core.Validations;

namespace RentACar.Domain
{
    public class Reservations : IReservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ReservationDate
        {
            get { return DateTime.Now; }
        }

        [Required]
        [FutureDate]
        public DateTime ReceivalDate { get; set; } 

        [Required]
        [EndWithinOneYear]
        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; } = default!;

        //Foreign Keys 

        [Required]
        public int VehicleId { get; set; }

        [Required]
        public int RenterId { get; set; }

        //Relations

        public Renter Renter { get; set; } = new Renter();

        public Vehicle Vehicle { get; set; } = new Vehicle();
    }
}
