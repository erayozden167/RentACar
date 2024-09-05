using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    public class Employers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;

        //fk
        [Required]
        public int UserId { get; set; }

        [Required]
        public int GarageId { get; set; }
        // Relations

        [DisallowNull]
        public Garage? Garage { get; set; }

        public User User { get; set; } = new User();

    }
}
