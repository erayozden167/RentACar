﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Domain
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Role { get; set; } = null!;

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Address { get; set; } = string.Empty;

        //fk
        [Required]
        public int UserId { get; set; }

        [Required]
        public int GarageId { get; set; }
        // Relations

        [DisallowNull]
        public Garage Garage { get; set; } = new Garage();

        public User User { get; set; } = new User();

    }
}