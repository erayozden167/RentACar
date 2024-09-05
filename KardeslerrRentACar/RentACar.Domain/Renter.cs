using RentACar.Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Domain
{
    public class Renter
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string GovIdNumber { get; set; } = string.Empty;

        [Required]
        public string LicenseType { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Gender { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Address { get; set; } = string.Empty;

        public int RentCount => Reservations?.Count(x => x.Status == "Valid") ?? 0;

        //Fk

        public int UserId { get; set; }


        // Relations
        public ICollection<Reservations> Reservations { get; set; } = new List<Reservations>();
        [Required]
        public User User { get; set; } = new User();


    }
}
