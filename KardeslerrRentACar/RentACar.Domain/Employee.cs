using System;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Domain
{
    public class Employee
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

        [Required]
        public string Role { get; set; } = string.Empty;

        public string? Gender { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Address { get; set; } = string.Empty;

        // Foreign Key
        [Required]
        public int UserId { get; set; }

        [Required]
        public int GarageId { get; set; }

        // Navigation Properties
        public Garage Garage { get; set; } = new Garage();
        public User User { get; set; } = new User();
    }
}
