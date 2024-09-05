using System;
using System.ComponentModel.DataAnnotations;
using RentACar.Core.Interfaces;

namespace RentACar.Core.Validations
{
    public class EndWithinOneYearAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is IReservation reservation)
            {
                var endDate = (DateTime)value;
                var receivalDate = reservation.ReceivalDate;

                if (endDate <= receivalDate.AddYears(1))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("End date must be within one year of the receival date.");
                }
            }

            return new ValidationResult("Invalid object type.");
        }
    }
}
