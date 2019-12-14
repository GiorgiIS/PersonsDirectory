using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TBCPersonsDirectory.Services.Dtos.PersonDtos;

namespace TBCPersonsDirectory.Services.Dtos.CustomValidationAttributes
{
    public class AtLeast18YearsOldAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var birthDate = DateTime.Parse(value.ToString());
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age)) age--;

            if (age >= 18)
            {
                return ValidationResult.Success;
            }

            var validationResult = new ValidationResult("Person should be at least 18 eyars old", new[] { nameof(PersonCreateDto.BirthDate) });

            return validationResult;
        }
    }
}
